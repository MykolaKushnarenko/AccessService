using DD.AccessService.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DD.AccessService.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(state: 0,
            operation: async (_, _) => { await _dbContext.SaveChangesAsync(cancellationToken); }, cancellationToken);
    }
}