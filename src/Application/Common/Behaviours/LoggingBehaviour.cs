using CoduTeam.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoduTeam.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IIdentityService _identityService;
    private readonly ILogger _logger;
    private readonly IUser _user;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("CoduTeam Request: {Name} {@UserId} {@Request}",
            requestName, _user.Id, request);

        return Task.CompletedTask;
    }
}
