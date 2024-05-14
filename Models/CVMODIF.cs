namespace APIcv.Models
{
    public class CVMODIF
    {
        public int ID { get; set; }
        public int IDCV { get; set; }
        public int IDUSER { get; set; }
        public DateTime DATE { get; set; }
        public CV CV { get; set; }
        public User User { get; set; }
    }
}
