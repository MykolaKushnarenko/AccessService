using DD.AccessService.ApplicationCore.Models;

namespace DD.AccessService.ApplicationCore.Features.GetApiKeys;

public class GetApiKeysQueryResponse
{
    public ApiKey[] ApiKeys { get; set; }
}