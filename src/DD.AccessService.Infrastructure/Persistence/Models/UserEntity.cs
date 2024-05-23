namespace DD.AccessService.Infrastructure.Persistence.Models;

public class UserEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public ICollection<ApiKeyEntity> ApiKeys { get; set; }
}