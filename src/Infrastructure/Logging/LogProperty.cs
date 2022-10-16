namespace Infrastructure.Logging;

public class LogProperty
{
    public string Name { get; set; } = string.Empty;
    public object? Value { get; set; } = null;
    public bool DestructValue { get; set; } = false;
}
