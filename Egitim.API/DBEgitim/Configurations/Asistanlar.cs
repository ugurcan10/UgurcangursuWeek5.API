using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.DBEgitim.Configurations
{
    public class Asistanlar : IEntityTypeConfiguration<Entites.Asistanlar>
    {
        public void Configure(EntityTypeBuilder<Entites.Asistanlar> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TC).HasMaxLength(11);
            builder.Property(x => x.Telno).HasMaxLength(11);
        }
    }
}
