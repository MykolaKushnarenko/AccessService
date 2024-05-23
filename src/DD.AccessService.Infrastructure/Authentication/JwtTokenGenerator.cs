using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DD.AccessService.ApplicationCore.Interfaces;
using DD.AccessService.ApplicationCore.Models;
using DD.AccessService.Infrastructure.Authentication.Configurations;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Claim = System.Security.Claims.Claim;

namespace DD.AccessService.Infrastructure.Authentication;

internal sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly ISystemClock _systemClock;
    private readonly JwtOptions _jwtOptions;
    
    public JwtTokenGenerator(
        ISystemClock systemClock, 
        IOptions<JwtOptions> jwtOptions)
    {
        _systemClock = systemClock;
        _jwtOptions = jwtOptions.Value;
    }
    
    public string Generate(User user, ApplicationCore.Models.Claim[] claims)
    {
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256);
        
        var tokenClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, $"{user.Name} {user.Surname}"),
            new(JwtRegisteredClaimNames.Iat, _systemClock.UtcNow.ToString())
        };
        tokenClaims.AddRange(claims.Select(claim => new Claim(claim.Name, claim.Value)));

        var securityToken = new JwtSecurityToken(claims: tokenClaims, signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}