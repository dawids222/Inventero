using LibLite.Inventero.Core.Contracts.Tools;
using Mapster;

namespace LibLite.Inventero.Service.Tools
{
    public class MapsterMapper : IMapper
    {
        public T Map<T>(object source) => source.Adapt<T>();
    }
}
