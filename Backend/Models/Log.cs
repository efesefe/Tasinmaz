using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_v3.Models
{
    public class Log
    {
        [Key]
        public int id { get; set; }
        /*[ForeignKey("Kullanici")]
        public int kullaniciID { get; set; }
        public virtual Kullanici Kullanici { get; set; }*/
        [ForeignKey("Durumlar")]
        public int durumID { get; set; }
        public virtual Durumlar Durumlar { get; set; }
        [ForeignKey("Islemler")]
        public int islemID { get; set; }
        public virtual Islemler Islemler { get; set; }
        public string aciklama { get; set; }
        public DateTime tarihSaat { get; set; }
        public string ip { get; set; }
    }
}