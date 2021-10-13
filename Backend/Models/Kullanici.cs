using System.ComponentModel.DataAnnotations;

namespace tasinmaz_v3.Models
{
    public class Kullanici
    {
        [Key]
        public int kullaniciID { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public bool role { get; set; } // 0 -> user && 1 -> admin
        public bool isActive { get; set; }
    }
}