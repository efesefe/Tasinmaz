using System.ComponentModel.DataAnnotations;

namespace tasinmaz_v3.Models
{
    public class Islemler
    {
        [Key]
        public int islemID { get; set; }
        public string islemAdi { get; set; }
    }
}