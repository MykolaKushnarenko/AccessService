namespace DD.AccessService.ApplicationCore.Models;

public class ApiKey
{
    public string Id { get; set; }
    
    public string Value { get; set; }
    
    public string UserId { get; set; }
    
    public DateTimeOffset LastUsedDateTime { get; set; }
    
    public Claim[] Claims { get; set; }
}