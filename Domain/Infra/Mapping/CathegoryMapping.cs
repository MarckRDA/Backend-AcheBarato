using Domain.Models.Cathegories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class CathegoryMapping : IEntityTypeConfiguration<Cathegory>
    {
        public void Configure(EntityTypeBuilder<Cathegory> builder)
        {
            builder
            .HasKey(cathegory => cathegory.Id);

            builder
            .Property(cathegory => cathegory.IdMLB)
            .IsRequired();

            builder
            .HasIndex(cathegory => cathegory.IdMLB)
            .IsUnique();

            builder
            .Property(cathegory => cathegory.NameMLB)
            .IsRequired();


        }
    }
}