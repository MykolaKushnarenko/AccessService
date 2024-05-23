using MediatR;

namespace DD.AccessService.ApplicationCore.Features.RevokeApiKey;

public class RevokeApiKeyCommand : IRequest<Unit>
{
    public string ApiKeyId { get; set; }
}