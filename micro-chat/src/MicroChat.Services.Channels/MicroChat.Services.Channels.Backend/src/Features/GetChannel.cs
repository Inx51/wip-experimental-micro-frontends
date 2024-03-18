using FastEndpoints;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Repositories;

namespace MicroChat.Services.Channels.Backend.Features;

public class GetChannelEndpoint : Endpoint<GetChannelRequestModel, Channel>
{
    private readonly ChannelRepository _channelRepository;
    
    public GetChannelEndpoint(ChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }
    
    public override void Configure()
    {
        Get("/channels/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetChannelRequestModel req, CancellationToken ct)
    {
        var entity = await _channelRepository.FindAsync(req.Id);
        await SendOkAsync(entity!, ct);
    }
}

public class GetChannelRequestModel
{
    public ulong Id { get; set; }
}