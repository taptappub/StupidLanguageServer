using DataAccess.EF.Generators;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Mappings;


public class UserMappings : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(x => x.Id)
            .UseIdentityColumn();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(4096);

        builder.Property(x => x.ExternalId)
            .HasValueGenerator<GuidValueGenerator>()
            .HasMaxLength(128)
            .IsRequired()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.Name)
            .HasMaxLength(512)
            .IsRequired();
    }
}
