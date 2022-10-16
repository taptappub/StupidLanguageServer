using Prometheus;

namespace DataAccess.EF.Repositories.Wrappers;

internal class BaseReadOnlyRepositoryMetricsWrapper<TEntity>
{
    private static readonly Histogram _methodDurationTimer = Metrics.CreateHistogram(
        "x_readonly_repository_duration", "Readonly repository duration",
        new HistogramConfiguration
        {
            LabelNames = new string[] { "Entity", "Method" }
        });

    protected ITimer GetTimer(string methodName) => 
        _methodDurationTimer
            .WithLabels(typeof(TEntity).Name, methodName)
            .NewTimer();
}
