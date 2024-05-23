using MediatR;

namespace DD.AccessService.ApplicationCore.Features.Authenticate;

public class AuthenticateCommand : IRequest<AuthenticateCommandResponse>
{
    public string ApiKey { get; set; } = null!;
}