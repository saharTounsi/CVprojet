namespace APIcv.Models
{
    public class VERSION
    {
        public int ID { get; set; }
        public int IDCV { get; set; }
        public DateTime DATE { get; set; }
        public string PATH { get; set; }
        public ICollection<CVEXPORTE> CVEXPORTEs { get; set; }
        public CV CV { get; set; }

    }
}
