using Mapster;

namespace Wemogy.Core.Mapster.Abstractions
{
    public interface IMappingConfig
    {
        void Configure(TypeAdapterConfig typeAdapterConfig);
    }
}
