namespace tasinmaz_V3.Models
{
    public class getTasinmazModel
    {
        public int tasinmazId { get; set; }
        public string tasinmazIl { get; set; }
        public string tasinmazIlce { get; set; }
        public string tasinmazMahalle { get; set; }
        public int ada { get; set; }
        public int parsel { get; set; }
        public string nitelik { get; set; }
        public string adres { get; set; }
        public bool isActive { get; set; } 
    }
}