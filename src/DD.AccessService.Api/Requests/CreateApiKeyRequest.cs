namespace DD.AccessServer.Api.Requests;

public class CreateApiKeyRequest
{
    public string UserId { get; set; }  = null!;
    public string[] Permissions { get; init; }
}