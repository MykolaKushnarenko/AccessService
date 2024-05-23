using DD.AccessService.ApplicationCore.Models;

namespace DD.AccessService.ApplicationCore.Interfaces;

public interface IUserStore
{
    public Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
}