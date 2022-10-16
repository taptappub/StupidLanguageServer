using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataAccess.EF.Generators;

public class GuidValueGenerator : ValueGenerator
{
    public override bool GeneratesTemporaryValues => false;

    public override object? Next(EntityEntry entry) => Guid.NewGuid();

    public override ValueTask<object?> NextAsync(EntityEntry entry, CancellationToken cancellationToken = default) => 
        ValueTask.FromResult((object?)Guid.NewGuid());

    protected override object? NextValue(EntityEntry entry) => Next(entry);

    protected override ValueTask<object?> NextValueAsync(EntityEntry entry, CancellationToken cancellationToken = default) => 
        NextAsync(entry, cancellationToken);
}
