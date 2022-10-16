using DataAccess.EF.Generators;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Mappings;

public class GroupMapping : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(nameof(Group));

        builder.Property(x => x.Id)
            .UseIdentityColumn();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .HasValueGenerator<GuidValueGenerator>()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(4096)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.RepetitionProgress)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .IsRequired();
    }
}
