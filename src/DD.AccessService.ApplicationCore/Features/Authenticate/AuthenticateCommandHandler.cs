using DD.AccessService.ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Internal;

namespace DD.AccessService.ApplicationCore.Features.Authenticate;

internal sealed class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>
{
    private readonly IApiKeyStore _apiKeyStore;
    private readonly IUserStore _userStore;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ISystemClock _systemClock; 

    public AuthenticateCommandHandler(
        IApiKeyStore apiKeyStore, 
        IJwtTokenGenerator jwtTokenGenerator,
        IUserStore userStore, 
        ISystemClock systemClock)
    {
        _apiKeyStore = apiKeyStore;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userStore = userStore;
        _systemClock = systemClock;
    }

    public async Task<AuthenticateCommandResponse> Handle(AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        //TODO: Validation
        
        var apikey = await _apiKeyStore.GetByApiKeyIncludingClaimsAsync(request.ApiKey, cancellationToken);
        var user = await _userStore.GetByIdAsync(apikey.UserId, cancellationToken);

        var token = _jwtTokenGenerator.Generate(user!, apikey.Claims);

        await _apiKeyStore.UpdateLastUsedDateTimeAsync(apikey.Id, _systemClock.UtcNow, cancellationToken);
        
        return new AuthenticateCommandResponse
        {
            Token = token
        };
    }
}