﻿namespace Readr.API.Services
{
    public interface ISmsService
    {
        Task<bool> Send(string phone, string message);
    }
}
