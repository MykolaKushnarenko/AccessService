using DD.AccessServer.Api.Responses;
using DD.AccessService.ApplicationCore.Features.CreateApiKey;

namespace DD.AccessServer.Api.Extensions;

internal static class CreateApiKeyCommandResponseExtensions
{
    public static CreateApiKeyResponse ToCreateApiKeyResponse(this CreateApiKeyCommandResponse result)
    {
        return new CreateApiKeyResponse()
        {
            ApiKey = result.ApiKey
        };
    }
}