using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
            .HasKey(product => product.Id);

            builder
            .Property(product => product.ProductIdMLB)
            .IsRequired();

            builder
            .Property(product => product.Name)
            .IsRequired();

            builder
            .Property(product => product.ThumbImgLink)
            .IsRequired();

            builder
            .Property(product => product.LinkRedirectShop)
            .IsRequired();

            builder
            .Property(product => product.Cathegory)
            .IsRequired();

            builder
            .Property(product => product.Tags)
            .IsRequired();
        }
    }
}