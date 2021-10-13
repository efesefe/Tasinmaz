using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_v3.Models
{
    public class Ilce
    {
        [Key]
        public int ilceID { get; set; }
        public string ilceName { get; set; }
        [ForeignKey("Il")]
        public int ilID { get; set; }
        public virtual Il Il { get; set; }
    }
}