namespace DD.AccessServer.Api.Responses;

public class GetApiKeysResponse
{
    public ApiKeyReposne[] ApiKeys { get; set; }
}

public class ApiKeyReposne
{
    public string Id { get; set; } = null!;
    
    public string Value { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    
    public DateTimeOffset LastUsedDateTime { get; set; }
    
    public string[] Permissions { get; set; }
}
