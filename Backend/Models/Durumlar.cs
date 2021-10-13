using System.ComponentModel.DataAnnotations;

namespace tasinmaz_v3.Models
{
    public class Durumlar
    {
        [Key]
        public int durumID { get; set; }
        public string durumAdi { get; set; }
    }
}