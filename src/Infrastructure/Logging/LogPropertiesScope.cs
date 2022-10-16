namespace Infrastructure.Logging;

class LogPropertiesScope : ILogPropertiesScope
{
    private readonly List<IDisposable> _properties = new List<IDisposable>(5);

    public void Add(IDisposable property)
    {
        _properties.Add(property);
    }

    public void Dispose()
    {
        foreach (var p in _properties)
        {
            p.Dispose();
        }
    }
}
