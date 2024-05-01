using System.ComponentModel.DataAnnotations;

namespace Readr.API.DTOs
{
    public record GetSecurityCodeDto
    {
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
