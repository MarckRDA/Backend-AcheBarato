using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class HistorycalPriceMapping : IEntityTypeConfiguration<HistorycalPrice>
    {
        public void Configure(EntityTypeBuilder<HistorycalPrice> builder)
        {
            builder
            .Property(hp => hp.DateOfPrice)
            .IsRequired();

            builder
            .Property(description => description.PriceOfThatDay)
            .IsRequired();
        }
    }
}