using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.DBEgitim.Configurations
{
    public class Egitimler : IEntityTypeConfiguration<Entites.Egitimler>
    {
        public void Configure(EntityTypeBuilder<Entites.Egitimler> builder)
        {
            builder.HasKey(x => x.Id);

            //Eğitimler tablosundaki EgitimAdi sütununun veri tipini nvarchar(50) yapmamızı sağladı.
            //Eğer ben bu sınırlandırmayı getirmeseydim veri tabanında bu tablonun veri tipi nvarchar(MAX) olacaktı.
            builder.Property(x => x.EgitimAdi).HasMaxLength(50);
        }
    }
}
