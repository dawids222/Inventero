using LibLite.Inventero.DAL.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibLite.Inventero.DAL.Converters
{
    internal class NullableDateTimeUtcConverter : ValueConverter<DateTime?, DateTime?>
    {
        public NullableDateTimeUtcConverter() : this(null) { }
        public NullableDateTimeUtcConverter(ConverterMappingHints hints) : base(
            datetime => datetime != null ? datetime.Value.ToUtc() : null,
            datetime => datetime != null ? datetime.Value.ToUtc() : null,
            hints)
        { }
    }
}
