using FastEndpoints;
using FastEndpoints.Swagger;
using MicroChat.Services.Channels.Backend;
using MicroChat.Services.Channels.Backend.Entities;
using MicroChat.Services.Channels.Backend.Hubs;
using MicroChat.Services.Channels.Backend.Repositories;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

const string serviceName = "MicroChat.Services.Channels.Backend";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddDefaultPolicy(b => b.AllowCredentials().SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<ChannelRepository>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<SeedBackgroundService>();

var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
var logBuilder = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console();

if (useOtlpExporter)
{
    logBuilder
        .WriteTo.OpenTelemetry(options =>
        {
            options.Endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
            options.ResourceAttributes.Add("service.name", "apiservice");
        });
}

Log.Logger = logBuilder.CreateBootstrapLogger();

builder.Logging.AddSerilog();

var otel = builder.Services.AddOpenTelemetry();

// Configure OpenTelemetry Resources with the application name
otel.ConfigureResource(resource => resource
    .AddService(serviceName: builder.Environment.ApplicationName));

// Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
otel.WithMetrics(metrics => metrics
    // Metrics provider from OpenTelemetry
    .AddAspNetCoreInstrumentation()
    // Metrics provides by ASP.NET Core in .NET 8
    .AddMeter("Microsoft.AspNetCore.Hosting")
    .AddMeter("Microsoft.AspNetCore.Server.Kestrel"));

// Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddSource(serviceName)
        .AddOtlpExporter(o => o.Endpoint = new Uri("http://localhost:16126"));
});

var app = builder.Build();
app.UseCors();
app.UseFastEndpoints();

app.MapHub<ChannelHub>("hubs/channel");

app.Run();