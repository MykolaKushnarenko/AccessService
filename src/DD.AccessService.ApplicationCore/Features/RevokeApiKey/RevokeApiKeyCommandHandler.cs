using DD.AccessService.ApplicationCore.Interfaces;
using MediatR;

namespace DD.AccessService.ApplicationCore.Features.RevokeApiKey;

internal sealed class RevokeApiKeyCommandHandler : IRequestHandler<RevokeApiKeyCommand, Unit>
{
    private readonly IApiKeyStore _apiKeyStore;
    private readonly IUnitOfWork _unitOfWork;
    
    public RevokeApiKeyCommandHandler(
        IApiKeyStore apiKeyStore, 
        IUnitOfWork unitOfWork)
    {
        _apiKeyStore = apiKeyStore;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(RevokeApiKeyCommand request, CancellationToken cancellationToken)
    {
        await _apiKeyStore.RemoveAsync(request.ApiKeyId, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}