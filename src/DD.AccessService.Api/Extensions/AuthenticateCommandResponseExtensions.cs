using DD.AccessServer.Api.Responses;
using DD.AccessService.ApplicationCore.Features.Authenticate;

namespace DD.AccessServer.Api.Extensions;

internal static class AuthenticateCommandResponseExtensions
{
    public static AuthenticateResponse ToAuthenticateResponse(this AuthenticateCommandResponse result)
    {
        return new AuthenticateResponse
        {
            Token = result.Token
        };
    }
}