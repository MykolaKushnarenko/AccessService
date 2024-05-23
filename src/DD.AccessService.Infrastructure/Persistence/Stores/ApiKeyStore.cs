using DD.AccessService.ApplicationCore.Interfaces;
using DD.AccessService.ApplicationCore.Models;
using DD.AccessService.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DD.AccessService.Infrastructure.Persistence.Stores;

internal sealed class ApiKeyStore : IApiKeyStore
{
    private readonly ApplicationDbContext _dbContext;

    public ApiKeyStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(ApiKey apiKey, CancellationToken cancellationToken = default) =>
        await _dbContext.ApiKeys.AddAsync(new ApiKeyEntity
        {
            Id = apiKey.Id,
            Value = apiKey.Value,
            UserId = apiKey.UserId,
            Claims = apiKey.Claims.Select(x => new ClaimEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = x.Name,
                Value = x.Value
            }).ToArray()
        }, cancellationToken);

    public async Task<ApiKey> GetByApiKeyIncludingClaimsAsync(string apiKey, CancellationToken cancellationToken = default)
    {
        var apikeyEntity = await _dbContext.ApiKeys
            .AsNoTracking()
            .Include(x => x.Claims)
            .FirstAsync(x => x.Value == apiKey, cancellationToken: cancellationToken);
        
        return new ApiKey
        {
            Id = apikeyEntity.Id,
            Value = apikeyEntity.Value,
            UserId = apikeyEntity.UserId,
            LastUsedDateTime = apikeyEntity.LastUsedDateTime,
            Claims = apikeyEntity.Claims.Select(c => new Claim
            {
                Name = c.Name,
                Value = c.Value
            }).ToArray()
        };
    }

    public Task<ApiKey[]> GetManyIncludingClaimsByUserIdAsync(string userId,
        CancellationToken cancellationToken = default) =>
        _dbContext.ApiKeys
            .Where(x => x.UserId == userId)
            .Select(x =>
                new ApiKey
                {
                    Id = x.Id,
                    Value = x.Value,
                    UserId = x.UserId,
                    LastUsedDateTime = x.LastUsedDateTime,
                    Claims = x.Claims.Select(c => new Claim
                    {
                        Name = c.Name,
                        Value = c.Value
                    }).ToArray()
                })
            .ToArrayAsync(cancellationToken: cancellationToken);

    public async Task UpdateLastUsedDateTimeAsync(string apiKeyId, DateTimeOffset dateTime,
        CancellationToken cancellationToken = default) =>
        await _dbContext.ApiKeys
            .Where(x => x.Id == apiKeyId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.LastUsedDateTime, dateTime),
                cancellationToken: cancellationToken);

    public async Task RemoveAsync(string apiKeyId, CancellationToken cancellationToken = default) =>
        await _dbContext.ApiKeys
            .Where(x => x.Id == apiKeyId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
}