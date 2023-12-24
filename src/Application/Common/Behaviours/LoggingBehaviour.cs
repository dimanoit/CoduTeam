using CoduTeam.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoduTeam.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        string? userName = string.Empty;

        if (_user.Id.HasValue)
        {
            userName = await _identityService.GetUserNameAsync(_user.Id.Value);
        }

        _logger.LogInformation("CoduTeam Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, _user.Id, userName, request);
    }
}
