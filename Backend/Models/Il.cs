using System.ComponentModel.DataAnnotations;

namespace tasinmaz_v3.Models
{
    public class Il
    {
        [Key]
        public int ilID { get; set; }
        public string ilName { get; set; }
    }
}