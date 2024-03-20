var builder = DistributedApplication.CreateBuilder(args);

var channelsBackend = builder.AddProject<Projects.MicroChat_Services_Channels_Backend>("channels-backend");

var channelsFrontend = builder.AddNpmApp("channels-frontend", AppDomain.CurrentDomain.BaseDirectory + "../../../../../../MicroChat.Services.Channels\\MicroChat.Services.Channels.Frontend\\src")
    .WithReference(channelsBackend);

builder.Build().Run();
