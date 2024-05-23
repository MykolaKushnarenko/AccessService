namespace DD.AccessService.Infrastructure.Persistence.Models;

public class ClaimEntity
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Value { get; set; }
    
    public string ApiKeyId { get; set; }
    
    public ApiKeyEntity ApiKey { get; set; }
}