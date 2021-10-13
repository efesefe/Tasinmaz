using Microsoft.EntityFrameworkCore;
using tasinmaz_v3.Models;

namespace tasinmaz_v3
{
    public class TasinmazDbContext : DbContext
    {
        public TasinmazDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Il> iller { get; set; }
        public DbSet<Ilce> ilceler { get; set; }
        public DbSet<Kullanici> kullanicilar { get; set; }
        public DbSet<Log> loglar { get; set; }
        public DbSet<Mahalle> mahalleler { get; set; }
        public DbSet<Tasinmaz> tasinmazlar { get; set; }
    }
}