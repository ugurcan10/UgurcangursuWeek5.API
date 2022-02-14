using Egitim.API.DBEgitim.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.DBEgitim
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Egitmenler> Egitmenler { get; set; }
        public DbSet<Asistanlar> Asistanlar { get; set; }
        public DbSet<Egitimler> Egitimler { get; set; }
        public DbSet<Katilimciler> Katilimciler { get; set; }
        public DbSet<Ogrenciler> Ogrenciler { get; set; }
        public DbSet<Yoneticiler> Yoneticiler { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //UseSQLServer metodunu kullanabilmek için package manager üzerinden Microsoft.EntityFrameworkCore.SqlServer paketini projemize eklememiz gerekiyor.
            //UserSqlServer metodu 2 farklı paramatre alıyor. Birinci parametremiz connectionstringimiz olacak.
            //İkinci parametre olarak ise expresion yöntemiyle Migration dosyasını hangi projeye eklemek istediğimizi yazıyoruz.
            optionsBuilder.UseSqlServer(DBEgitim.ConnectionStrings.EgitimSQLServer.Text, x => x.MigrationsAssembly("Egitim.API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bu kod çalıştığında proje içerisinde IEntityTypeConfiguration interface'inden miras alan bir class var mı ona bakıyor.
            //Sonrasında o class içerisinde Configure metodu implement edilmiş mi bunun kontrolü yapılır.
            //Eğer o da implement edilmişse artık Configure metodumun içerisindeki kurallar tablo sütunlarına uygulanıyor.
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
