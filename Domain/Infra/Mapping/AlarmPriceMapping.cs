using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class AlarmPriceMapping : IEntityTypeConfiguration<AlarmPrice>
    {
        public void Configure(EntityTypeBuilder<AlarmPrice> builder)
        {
            builder
            .HasKey(product => product.Id);

            builder
            .Property(product => product.WishPrice)
            .IsRequired();


        }
    }
}