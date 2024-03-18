using MicroChat.Services.Channels.Backend.Entities;

namespace MicroChat.Services.Channels.Backend.Repositories;

public class ChannelRepository
{
    public List<Channel> Channels { get; } = [];
    
    private ulong _lastId = 0;
    
    public Task AddAsync(Channel channel)
    {
        channel.Id = _lastId;
        Channels.Add(channel);
        _lastId++;
        return Task.CompletedTask;
    }
    
    public async Task DeleteAsync(ulong id)
    {
        var channel = await FindAsync(id);
        Channels.Remove(channel!);
    }
    
    public Task<Channel?> FindAsync(ulong id)
    {
        var channel = Channels.Find(c => c.Id == id);
        return Task.FromResult(channel);
    }
    
    public Task<IEnumerable<Channel>> AllAsync()
    {
        return Task.FromResult(Channels.AsEnumerable());
    }
}