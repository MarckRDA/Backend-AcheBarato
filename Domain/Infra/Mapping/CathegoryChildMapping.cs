using Domain.Models.Cathegories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class CathegoryChildMapping : IEntityTypeConfiguration<CathegoryChild>
    {
        public void Configure(EntityTypeBuilder<CathegoryChild> builder)
        {
            builder
            .Property(cathegorychild => cathegorychild.IdMLB)
            .IsRequired();

            builder
            .HasIndex(cathegorychild => cathegorychild.IdMLB)
            .IsUnique();

            builder
            .Property(cathegorychild => cathegorychild.NameMLB)
            .IsRequired();
        }
    }
}