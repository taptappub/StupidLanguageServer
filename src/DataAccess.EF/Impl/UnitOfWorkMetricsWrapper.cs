using System.Diagnostics.Metrics;
using Prometheus;

namespace DataAccess.EF.Impl;

public class UnitOfWorkMetricsWrapper : IUnitOfWork
{
    private static readonly Histogram _saveChangesDuration = Metrics.CreateHistogram(
        "x_save_changes_duration", "Save changes duration");

    private readonly IUnitOfWork _unitOfWork;

    public IRepositoryFactory Repositories => _unitOfWork.Repositories;

    public UnitOfWorkMetricsWrapper(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        using (_saveChangesDuration.NewTimer())
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public ValueTask DisposeAsync() => _unitOfWork.DisposeAsync();
}
