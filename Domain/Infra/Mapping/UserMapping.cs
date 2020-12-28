using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .Property(user => user.Name)
            .IsRequired();

            builder
            .Property(user => user.Email)
            .IsRequired();

            builder
            .HasIndex(user => user.Email)
            .IsUnique();

            builder
            .Property(user => user.Password)
            .IsRequired();

        }
    }
}