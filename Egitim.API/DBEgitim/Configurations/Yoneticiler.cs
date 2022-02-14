using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egitim.API.DBEgitim.Configurations
{
    public class Yoneticiler : IEntityTypeConfiguration<Entites.Yoneticiler>
    {
        public void Configure(EntityTypeBuilder<Entites.Yoneticiler> builder)
        {
            builder.Property(x => x.NameSurname).HasMaxLength(100);
            builder.Property(x => x.TelNo).HasMaxLength(11);
            builder.Property(x => x.Username).HasMaxLength(30);
            builder.Property(x => x.Password).HasMaxLength(30);

            builder.Property(x => x.NameSurname).IsRequired();
            builder.Property(x => x.TelNo).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
