using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Readr.API.Data;
using Readr.API.Hubs;
using Readr.API.Services;
using Readr.API.Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddLogging();


builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtGenerationService, JwtGenerationService>();
builder.Services.AddScoped<IUserService, UserService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<ISmsService, TwilioSmsService>();
} 
else
{
    builder.Services.AddSingleton<ISmsService, TwilioSmsService>();
}

builder.Services.AddScoped<IBookCoverService, WeslleyBookCoverService>();


builder.Services.AddSingleton<AddCoverQueue, AddCoverQueue>();

builder.Services.AddHostedService<BookProcessor>();
builder.Services.AddHostedService<MutualLikesService>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:Key"]!)
            )
        };
        options.SaveToken = true;
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/Notification")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<ReadrDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("mainDb") ?? throw new InvalidOperationException("Connection string 'mainDb' not found."))
    );


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        ReadrDbContext dbContext = scope.ServiceProvider.GetRequiredService<ReadrDbContext>();

        await SeedData.SeedAsync(dbContext);
    }
}

app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(builder.Configuration["CorsOrigins"]!);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/Notification");

app.Run();
