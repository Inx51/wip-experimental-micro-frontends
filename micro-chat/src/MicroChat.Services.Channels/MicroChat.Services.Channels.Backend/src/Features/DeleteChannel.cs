using FastEndpoints;
using MicroChat.Services.Channels.Backend.Hubs;
using MicroChat.Services.Channels.Backend.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace MicroChat.Services.Channels.Backend.Features;

public class DeleteChannelEndpoint : Endpoint<DeleteChannelRequestModel>
{
    private readonly ChannelRepository _channelRepository;
    private readonly IHubContext<ChannelHub> _hub;
    
    public DeleteChannelEndpoint(ChannelRepository channelRepository, IHubContext<ChannelHub> hub)
    {
        _channelRepository = channelRepository;
        _hub = hub;
    }
    
    public override void Configure()
    {
        Delete("/channels/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteChannelRequestModel req, CancellationToken ct)
    {
        await _channelRepository.DeleteAsync(req.Id);
        await _hub.Clients.All.SendAsync
        (
            "ChannelDeleted",
            req.Id,
            cancellationToken: ct
        );
        await SendNoContentAsync(ct);
    }
}

public class DeleteChannelRequestModel
{
    public ulong Id { get; set; }
}