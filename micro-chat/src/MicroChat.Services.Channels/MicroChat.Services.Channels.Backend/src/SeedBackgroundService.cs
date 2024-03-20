using Bogus;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Repositories;

namespace MicroChat.Services.Channels.Backend;

public class SeedBackgroundService : BackgroundService
{
    private const int NumOfChannels = 4;
    
    private readonly ChannelRepository _channelRepository;
    private readonly List<string> _alreadyAdded = [];
    
    public SeedBackgroundService(ChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var faker = new Faker();
        for (var i = 0; i < NumOfChannels; i++)
        {
            var name = faker.Music.Genre();
            var image = faker.Image.LoremFlickrUrl(320, 320, $"{name} music");

            if (_alreadyAdded.Contains(name))
            {
                i--;
                continue;
            }
            
            var channel = new Channel
            {
                Name = name,
                Image = image
            };
        
            await _channelRepository.AddAsync(channel);
            _alreadyAdded.Add(name);
        }
    }
}