namespace MicroChat.Services.Channels.Backend.Entities;

public class Channel
{
    public ulong Id { get; set; }
    
    public string? Name { get; set; }

    public string? Image { get; set; }
}