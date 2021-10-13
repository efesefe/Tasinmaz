namespace tasinmaz_V3.Models
{
    public class userUnHashed
    {
        public int kullaniciID { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool role { get; set; } // 0 -> user && 1 -> admin
        public bool isActive { get; set; }
    }
}