using DD.AccessService.ApplicationCore.Interfaces;
using DD.AccessService.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DD.AccessService.Infrastructure.Persistence.Stores;

internal sealed class UserStore : IUserStore
{
    private readonly ApplicationDbContext _dbContext;
    
    public UserStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var userEntity = await _dbContext.Users.FirstAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        
        return new User
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Surname = userEntity.Surname
        };
    }
}