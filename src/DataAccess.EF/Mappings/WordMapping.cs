using DataAccess.EF.Generators;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Mappings;

public class WordMapping : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.ToTable(nameof(Word));

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .HasValueGenerator<GuidValueGenerator>().ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Value)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.RepetitionProgress)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(8192);

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(4096);

        builder.HasOne(x => x.Group)
            .WithMany();

        builder.HasOne(x => x.User)
            .WithMany();

        builder.Property(x => x.IsDeleted)
            .IsRequired();
    }
}
