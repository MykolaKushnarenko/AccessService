using DD.AccessService.ApplicationCore.Interfaces;
using DD.AccessService.ApplicationCore.Models;
using MediatR;

namespace DD.AccessService.ApplicationCore.Features.CreateApiKey;

internal sealed class CreateApiKeyCommandHandler : IRequestHandler<CreateApiKeyCommand, CreateApiKeyCommandResponse>
{
    private readonly IUserStore _userStore;
    private readonly IApiKeyStore _apiKeyStore;
    private readonly IUnitOfWork _unitOfWork;

    public CreateApiKeyCommandHandler(
        IUserStore userStore,
        IApiKeyStore apiKeyStore,
        IUnitOfWork unitOfWork)
    {
        _userStore = userStore;
        _apiKeyStore = apiKeyStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateApiKeyCommandResponse> Handle(CreateApiKeyCommand request,
        CancellationToken cancellationToken)
    {
        //TODO: Validation

        var user = await _userStore.GetByIdAsync(request.UserId, cancellationToken);

        var apiKey = PopulateApiKey(request, user);

        await _apiKeyStore.CreateAsync(apiKey, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var createdApiKey = await _apiKeyStore.GetByApiKeyIncludingClaimsAsync(apiKey.Value, cancellationToken);

        return new CreateApiKeyCommandResponse
        {
            ApiKey = createdApiKey.Value
        };
    }

    private static ApiKey PopulateApiKey(CreateApiKeyCommand request, User? user)
    {
        const string permissionClaimName = "Permission";
        
        var apiKey = new ApiKey
        {
            Id = Guid.NewGuid().ToString(),
            Value = $"AS_{Guid.NewGuid().ToString()}",
            UserId = user!.Id,
            Claims = request.Permissions.Select(p => new Claim
            {
                Name = permissionClaimName,
                Value = p
            }).ToArray()
        };
        return apiKey;
    }
}