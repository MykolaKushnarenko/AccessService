using MediatR;

namespace DD.AccessService.ApplicationCore.Features.GetApiKeys;

public class GetApiKeysQuery : IRequest<GetApiKeysQueryResponse>
{
    public string UserId { get; set; } = null!;
}