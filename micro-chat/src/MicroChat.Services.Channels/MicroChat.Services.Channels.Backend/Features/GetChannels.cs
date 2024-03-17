using FastEndpoints;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Repositories;

namespace MicroChat.Services.Channels.Backend.Features;

public class GetChannelsEndpoint : EndpointWithoutRequest<IEnumerable<Channel>>
{
    private readonly ChannelRepository _channelRepository;

    public GetChannelsEndpoint(ChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }

    public override void Configure()
    {
        Get("/channels/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var entities = await _channelRepository.AllAsync();
        await SendOkAsync(entities, ct);
    }
}