using Mapster;
using Wemogy.Core.Mapster.Abstractions;
using Wemogy.Core.Tests.Mapster.TestResources.Models;

namespace Wemogy.Core.Tests.Mapster.TestResources.MappingConfigs;

public class UserMemberMappingConfig : IMappingConfig
{
    public void Configure(TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig
            .NewConfig<User, Member>();
    }
}
