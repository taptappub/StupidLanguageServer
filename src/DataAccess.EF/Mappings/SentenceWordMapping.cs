using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Mappings;

public class SentenceWordMapping : IEntityTypeConfiguration<SentenceWord>
{
    public void Configure(EntityTypeBuilder<SentenceWord> builder)
    {
        builder.ToTable(nameof(SentenceWord));

        builder.Property(x => x.Id)
            .UseIdentityColumn();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderedNumber)
            .IsRequired();
            
        builder.HasOne(x => x.Sentence)
            .WithMany(x => x.Words)
            .HasForeignKey("SentenceId")
            .IsRequired();

        builder.HasOne(x => x.Word)
            .WithMany()
            .HasForeignKey("WordId")
            .IsRequired();
    }
}
