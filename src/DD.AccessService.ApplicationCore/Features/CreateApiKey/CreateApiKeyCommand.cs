using MediatR;

namespace DD.AccessService.ApplicationCore.Features.CreateApiKey;

public class CreateApiKeyCommand : IRequest<CreateApiKeyCommandResponse>
{
    public string UserId { get; init; }  = null!;
    
    public string[] Permissions { get; init; }
}