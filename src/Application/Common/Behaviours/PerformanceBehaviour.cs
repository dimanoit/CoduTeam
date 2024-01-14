using System.Diagnostics;
using CoduTeam.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoduTeam.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;
    private readonly IUser _user;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        IUser user,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        TResponse response = await next();

        _timer.Stop();

        long elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            string requestName = typeof(TRequest).Name;
            string? userName = string.Empty;

            if (_user.Id.HasValue)
            {
                userName = await _identityService.GetUserNameAsync(_user.Id.Value);
            }

            _logger.LogWarning(
                "CoduTeam Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, _user.Id, userName, request);
        }

        return response;
    }
}
