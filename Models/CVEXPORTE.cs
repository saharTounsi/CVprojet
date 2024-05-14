using System.ComponentModel.DataAnnotations;

namespace APIcv.Models
{
    public class CVEXPORTE
    {
        [Required]
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDVERSION { get; set; }
        public int IDCV { get; set; }
        public DateTime DATEEXPORT { get; set; }
        public CV CV { get; set; }
       
        public User User { get; set; }
        public Version Version { get; set; }
     
        

    }
}
