using LibLite.Inventero.DAL.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibLite.Inventero.DAL.Converters
{
    internal class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeUtcConverter() : this(null) { }
        public DateTimeUtcConverter(ConverterMappingHints hints) : base(
            datetime => datetime.ToUtc(),
            datetime => datetime.ToUtc(),
            hints)
        { }
    }
}
