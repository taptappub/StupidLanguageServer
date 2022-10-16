using DataAccess.EF.Generators;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Mappings;

public class SentenceMapping : IEntityTypeConfiguration<Sentence>
{
    public void Configure(EntityTypeBuilder<Sentence> builder)
    {
        builder.ToTable(nameof(Sentence));

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .HasValueGenerator<GuidValueGenerator>()
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(8192)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.Ignore(x => x.OrderedWords);
    }
}
