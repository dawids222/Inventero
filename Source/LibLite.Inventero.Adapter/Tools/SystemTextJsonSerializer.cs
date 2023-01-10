using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Service.Converters;
using System.Text.Json;

namespace LibLite.Inventero.Service.Tools
{
    public class SystemTextJsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer()
        {
            _options = new()
            {
                PropertyNameCaseInsensitive = true,
            };
            var coverter = new CultureSpecificQuotedDecimalConverter();
            _options.Converters.Add(coverter);
        }

        public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _options);
        public string Serialize(object value) => JsonSerializer.Serialize(value, _options);
    }
}
