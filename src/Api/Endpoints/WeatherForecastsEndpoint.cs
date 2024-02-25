using CoduTeam.Api.Infrastructure;
using CoduTeam.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace CoduTeam.Api.Endpoints;

public class WeatherForecastsEndpoint : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetWeatherForecasts);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
