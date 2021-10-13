using System.Collections.Generic;
using tasinmaz_v3.Models;

namespace tasinmaz_V3.Models
{
    public class TasinmazVeSayfaSayisi
    {
        public List<getTasinmazModel> tasinmazlar;
        public int pages;
        public TasinmazVeSayfaSayisi()
        {
            tasinmazlar = new List<getTasinmazModel>();
            pages = 0;
        }
    }
}