using System.ComponentModel.DataAnnotations;

namespace APIcv.Dto.Account
{
    public class RegisterDto
    {
         public class ResisterDto
        {
            [Required]
            public string? Username { get; set; }

            [Required]
            public string? firstName { get; set; }

            [Required]
            public string? lastName { get; set; }

            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            [Required]
            public string? Password { get; set; }
        }
    }
}
