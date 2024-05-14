using System.ComponentModel.DataAnnotations;

namespace APIcv.Dto
{
    public class CVDto
    {
        [Required]
        public int ID { get; set; }
        public DateTime dateTime { get; set; }
        public string ETAT { get; set; }
    }
}
