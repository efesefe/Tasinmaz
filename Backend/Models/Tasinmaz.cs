using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_v3.Models
{
    public class Tasinmaz
    {
        [Key]
        public int tasinmazID { get; set; }        
        public int mahalleID { get; set; }
        [ForeignKey("mahalleID")]
        public virtual Mahalle Mahalle { get; set; }
        public int Ada { get; set; }
        public int Parsel { get; set; }
        public string nitelik { get; set; }
        public string adres { get; set; }
        public bool isActive { get; set; }
    }
}