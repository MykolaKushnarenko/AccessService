using DD.AccessServer.Api.Responses;
using DD.AccessService.ApplicationCore.Features.GetApiKeys;

namespace DD.AccessServer.Api.Extensions;

internal static class GetApiKeysQueryResponseExtensions
{
    public static GetApiKeysResponse ToGetApiKeysResponse(this GetApiKeysQueryResponse result)
    {
        return new GetApiKeysResponse
        {
            ApiKeys = result.ApiKeys.Select(x => new ApiKeyReposne
            {
                Id = x.Id,
                Value = x.Value,
                UserId = x.UserId,
                LastUsedDateTime = x.LastUsedDateTime,
                Permissions = x.Claims.Select(c => c.Value).ToArray()
            }).ToArray()
        };
    }
}