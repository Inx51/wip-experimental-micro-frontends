using FastEndpoints;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Hubs;
using MicroChat.Services.Channels.Backend.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace MicroChat.Services.Channels.Backend.Features;

public class CreateChannelEndpoint : Endpoint<CreateChannelRequestModel>
{
    private readonly ChannelRepository _channelRepository;
    private readonly IHubContext<ChannelHub> _hub;
    
    public CreateChannelEndpoint(ChannelRepository channelRepository, IHubContext<ChannelHub> hub)
    {
        _channelRepository = channelRepository;
        _hub = hub;
    }
    
    public override void Configure()
    {
        Post("/channels/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateChannelRequestModel req, CancellationToken ct)
    {
        var channel = MapToEntity(req);
        await _channelRepository.AddAsync(channel);
        await _hub.Clients.All.SendAsync
        (
            "ChannelCreated",
            channel,
            cancellationToken: ct
        );
        await SendCreatedAtAsync<GetChannelEndpoint>
        (
            new { id = channel.Id},
            new { channel.Id },
            cancellation:ct
        );
    }

    private Channel MapToEntity(CreateChannelRequestModel req) => new()
    {
        Name = req.Name,
        Image = req.Image
    };
}

public class CreateChannelRequestModel
{
    public string? Name { get; set; }

    public string? Image { get; set; }
}