using ITISHub.Core.Enums;

namespace ITISHub.Persistence.Entities;

public class ResourceEntity
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public ResourceType Type { get; set; }
}
