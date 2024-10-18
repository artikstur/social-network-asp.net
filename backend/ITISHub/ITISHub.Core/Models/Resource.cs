using ITISHub.Core.Enums;

namespace ITISHub.Core.Models;

public class Resource
{
    public Guid Id { get; set; }
    public string Url { get; set; } 
    public ResourceType Type { get; set; }

    private Resource(Guid id, string url, ResourceType type)
    {
        Id = id;
        Url = url;
        Type = type;
    }

    public static Resource Create(string url, ResourceType type)
    {
        var id = Guid.NewGuid();

        return new Resource(id, url, type);
    }
}