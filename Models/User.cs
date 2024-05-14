using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace APIcv.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }
        public int IDADMIN { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public ICollection<CV> CVs { get; set; }
        public ICollection< CVEXPORTE> CVEXPORTEs { get; set; }
        public ICollection<CVMODIF> CVMODIFs { get; set; }
    }
}
