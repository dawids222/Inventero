using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using Mapster;

namespace LibLite.Inventero.Adapter.Tools
{
    public class MapsterMapper : IMapper
    {
        private readonly TypeAdapterConfig _config;

        public MapsterMapper()
        {
            _config = CreateConfiguration();
        }

        public T Map<T>(object source) => source.Adapt<T>(_config);

        private static TypeAdapterConfig CreateConfiguration()
        {
            var config = new TypeAdapterConfig();
            config.ForDestinationType<Purchase>().MapToConstructor(true);
            config.ForDestinationType<Product>().MapToConstructor(true);
            config.ForDestinationType<Group>().MapToConstructor(true);
            return config;
        }
    }
}
