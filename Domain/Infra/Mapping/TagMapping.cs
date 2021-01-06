using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
            .Property(tag => tag.Name)
            .IsRequired();

        }
    }
}