using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace WebAPI.Infrastructure;

public class UtcTimestampColumnWriter : ColumnWriterBase
{
    public UtcTimestampColumnWriter(NpgsqlDbType dbType = NpgsqlDbType.TimestampTz) : base(dbType)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        return logEvent.Timestamp.DateTime.ToUniversalTime();
    }
}
