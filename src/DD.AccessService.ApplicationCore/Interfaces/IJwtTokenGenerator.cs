using DD.AccessService.ApplicationCore.Models;

namespace DD.AccessService.ApplicationCore.Interfaces;

public interface IJwtTokenGenerator
{
    public string Generate(User user, Claim[] claims);
}