using DD.AccessService.ApplicationCore.Interfaces;
using MediatR;

namespace DD.AccessService.ApplicationCore.Features.GetApiKeys;

internal sealed class GetApiKeysQueryHandler : IRequestHandler<GetApiKeysQuery, GetApiKeysQueryResponse>
{
    private readonly IApiKeyStore _apiKeyStore;

    public GetApiKeysQueryHandler(IApiKeyStore apiKeyStore)
    {
        _apiKeyStore = apiKeyStore;
    }

    public async Task<GetApiKeysQueryResponse> Handle(GetApiKeysQuery request, CancellationToken cancellationToken)
    {
        var keys = await _apiKeyStore.GetManyIncludingClaimsByUserIdAsync(request.UserId, cancellationToken);

        return new GetApiKeysQueryResponse
        {
            ApiKeys = keys
        };
    }
}