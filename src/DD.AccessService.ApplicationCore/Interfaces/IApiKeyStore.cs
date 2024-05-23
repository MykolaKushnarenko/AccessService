using DD.AccessService.ApplicationCore.Models;

namespace DD.AccessService.ApplicationCore.Interfaces;

public interface IApiKeyStore
{
    public Task CreateAsync(ApiKey apiKey, CancellationToken cancellationToken = default);

    public Task<ApiKey> GetByApiKeyIncludingClaimsAsync(string apiKey, CancellationToken cancellationToken = default);

    public Task<ApiKey[]> GetManyIncludingClaimsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    public Task UpdateLastUsedDateTimeAsync(string apiKeyId, DateTimeOffset dateTime, CancellationToken cancellationToken = default);

    public Task RemoveAsync(string apiKeyId, CancellationToken cancellationToken = default);
}