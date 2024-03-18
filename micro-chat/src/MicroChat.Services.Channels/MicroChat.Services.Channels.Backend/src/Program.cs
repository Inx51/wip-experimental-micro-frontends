using FastEndpoints;
using FastEndpoints.Swagger;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Hubs;
using MicroChat.Services.Channels.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<ChannelRepository>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseFastEndpoints();

app.MapHub<ChannelHub>("hubs/channel");

SeedChannels();

app.Run();

void SeedChannels()
{
    List<Channel> channels =
    [
        new Channel
        {
            Name = "Food",
            Image = ""
        },
        new Channel
        {
            Name = "Gaming",
            Image = ""
        },
        new Channel
        {
            Name = "Animals",
            Image = ""
        },
        new Channel
        {
            Name = "Cars",
            Image = ""
        }
    ];
    
    var repository = app.Services.GetRequiredService<ChannelRepository>();
    foreach (var channel in channels)
    {
        repository.AddAsync(channel);
    }
}