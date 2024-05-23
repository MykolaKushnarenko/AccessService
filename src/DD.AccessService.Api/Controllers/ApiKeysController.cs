using DD.AccessServer.Api.Extensions;
using DD.AccessServer.Api.Requests;
using DD.AccessService.ApplicationCore.Features.Authenticate;
using DD.AccessService.ApplicationCore.Features.CreateApiKey;
using DD.AccessService.ApplicationCore.Features.GetApiKeys;
using DD.AccessService.ApplicationCore.Features.RevokeApiKey;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DD.AccessServer.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Route("api/[controller]")]
public class ApiKeysController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public ApiKeysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>    
    ///     Create an api key.    
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    ///
    /// <code>
    /// POST api/apikeys 
    /// {  
    ///     "userId": "ADA96D6D-7214-40BE-BC76-A4741A879D8E",
    ///     "permissions": ["Permission1"]
    /// }
    /// </code>    
    ///    
    /// </remarks>    
    /// <returns>A newly created games</returns>    
    /// <response code="200">Returns the newly created item</response>    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateApiKeyRequest request)
    {
        var result = await _mediator.Send(new CreateApiKeyCommand
        {
            UserId = request.UserId,
            Permissions = request.Permissions
        });

        return Ok(result.ToCreateApiKeyResponse());
    }
    
    /// <summary>    
    ///     Authenticate a user with api key.    
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    ///    
    /// POST api/apikeys/authenticate
    /// {    
    ///     "apiKey": "AS_3f13f8a3-92f7-4c71-be19-eb4ae70eca89" 
    /// }
    ///    
    /// </remarks>    
    /// <response code="200"></response>    
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest request)
    {
        var result = await _mediator.Send(new AuthenticateCommand
        {
            ApiKey = request.ApiKey
        });

        return Ok(result.ToAuthenticateResponse());
    }
    
    /// <summary>    
    ///     Delete an api key.    
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    ///
    /// <code>
    /// DELETE api/apikeys/7bb6e8bb-fa4e-4063-b542-e56aee3b9e2e  
    /// </code>    
    ///    
    /// </remarks>    
    /// <response code="200">Returns the newly created item</response>    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Authenticate(string id)
    {
        await _mediator.Send(new RevokeApiKeyCommand
        {
            ApiKeyId = id
        });

        return Ok();
    }
    
    /// <summary>    
    ///     Gets all user api keys.    
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    /// <code>
    /// GET api/apikeys/ADA96D6D-7214-40BE-BC76-A4741A879D8E    
    /// </code>   
    /// </remarks>    
    /// <returns>A set of api keys.</returns>    
    /// <response code="200">Returns the items.</response>    
    [HttpGet]
    public async Task<IActionResult> GetUserApiKeys([FromHeader] string userId)
    {
        var result = await _mediator.Send(new GetApiKeysQuery
        {
            UserId = userId
        });

        return Ok(result.ToGetApiKeysResponse());
    }
}