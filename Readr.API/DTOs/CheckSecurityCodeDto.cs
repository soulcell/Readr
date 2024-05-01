using System.ComponentModel.DataAnnotations;

namespace Readr.API.DTOs
{
    public record CheckSecurityCodeDto
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
