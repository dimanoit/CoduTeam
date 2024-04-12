using CoduTeam.Api;
using CoduTeam.Application;
using CoduTeam.Infrastructure;
using CoduTeam.Infrastructure.Data;
using CoduTeam.Infrastructure.Hubs;
using DependencyInjection = CoduTeam.Api.DependencyInjection;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Debug()
    .WriteTo.Console()
    .CreateLogger();

var host = builder.Host;
host.UseSerilog();

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));
app.MapHub<ChatHub>("/chat-hub");

app.MapEndpoints();
app.UseCors(DependencyInjection.CorsPolicyName);

app.Run();

public partial class Program
{
}
