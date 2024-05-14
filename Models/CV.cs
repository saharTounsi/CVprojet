using System.ComponentModel.DataAnnotations;

namespace APIcv.Models
{
    public class CV
    {
        [Required]
        public int ID { get; set; }
        public int IDUser { get; set; }
        public DateTime dateTime { get; set; }
        public string ETAT { get; set; }
        public string PATH { get; set; }
        public User user { get; set; }
        public ICollection<CVMODIF> CVMODIFs { get; set; }
        public ICollection<CVEXPORTE> CVEXPORTEs { get; set; }
        public ICollection<VERSION> VERSIONs { get; set; }
    }
}
