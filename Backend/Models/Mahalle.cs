using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_v3.Models
{
    public class Mahalle
    {
        [Key]
        public int mahalleID { get; set; }
        public string mahalleName { get; set; }
        [ForeignKey("Ilce")]
        public int ilceID { get; set; }
        public virtual Ilce Ilce { get; set; }
    }
}