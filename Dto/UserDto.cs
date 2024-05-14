using System.ComponentModel.DataAnnotations;

namespace APIcv.Dto
{
    public class UserDto
    {
        [Required]
        public int ID { get; set; }
        public string Login { get; set; }
        [Required]
        public string PWD { get; set; }
        public string Status { get; set; }
        [Required]
        public string Role { get; set; }
    }
}


