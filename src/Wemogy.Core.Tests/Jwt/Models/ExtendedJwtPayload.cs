namespace Wemogy.Core.Tests.Jwt.Models;

public class ExtendedJwtPayload
{
    public string SpaceBlocksTenantId { get; set; }

    public string SpaceBlocksProjectId { get; set; }

    public ExtendedJwtPayload()
    {
        SpaceBlocksTenantId = string.Empty;
        SpaceBlocksProjectId = string.Empty;
    }
}
