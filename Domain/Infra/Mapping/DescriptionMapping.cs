using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class DescriptionMapping : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder
            .Property(description => description.IdMLB)
            .IsRequired();

            builder
            .Property(description => description.Value)
            .IsRequired();

        }
    }
}