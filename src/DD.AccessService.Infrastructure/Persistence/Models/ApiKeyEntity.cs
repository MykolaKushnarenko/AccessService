namespace DD.AccessService.Infrastructure.Persistence.Models;

public class ApiKeyEntity
{
    public string Id { get; set; }
    
    public string Value { get; set; }
    
    public string UserId { get; set; }
    
    public DateTimeOffset LastUsedDateTime { get; set; }

    public ICollection<ClaimEntity> Claims { get; set; }
}