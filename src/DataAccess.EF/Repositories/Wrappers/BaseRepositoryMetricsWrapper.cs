using Domain.Repositories;
using Prometheus;

namespace DataAccess.EF.Repositories.Wrappers;

internal class BaseRepositoryMetricsWrapper<TEntity> where TEntity : class
{
    private static readonly Histogram _methodDurationTimer = Metrics.CreateHistogram(
        "x_repository_duration", "Repository duration",
        new HistogramConfiguration
        {
            LabelNames = new string[] { "Entity", "Method" },
            //StaticLabels = new Dictionary<string, string> { { "Entity", typeof(TEntity).Name } }
        });

    private readonly IBaseRepository<TEntity> _repository;

    protected ITimer GetTimer(string methodName) =>
        _methodDurationTimer
            .WithLabels(typeof(TEntity).Name, methodName)
            .NewTimer();

    public BaseRepositoryMetricsWrapper(IBaseRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Add(TEntity entity)
    {
        using (GetTimer(nameof(Add)))
        {
            _repository.Add(entity);
        }
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        using (GetTimer(nameof(AddRange)))
        {
            _repository.AddRange(entities);
        }
    }

    public void Remove(TEntity entity)
    {
        using (GetTimer(nameof(Remove)))
        {
            _repository.Remove(entity);
        }
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        using (GetTimer(nameof(RemoveRange)))
        {
            _repository.RemoveRange(entities);
        }
    }
}
