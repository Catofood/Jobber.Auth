using Api.Options;
using Jobber.Auth.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ApiCookieOptions _apiCookieOptions;

    public AuthController(IMediator mediator, IOptions<ApiCookieOptions> cookieOptions)
    {
        _mediator = mediator;
        _apiCookieOptions = cookieOptions.Value;
    }

    
    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var jwtToken = await _mediator.Send(command, cancellationToken);
        Response.Cookies.Append(_apiCookieOptions.AccessTokenCookieName, jwtToken);
        return Ok();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] Guid userId, 
        [FromQuery] string confirmationToken, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("test")]
    public async Task<IActionResult> Test(CancellationToken cancellationToken)
    {
        return Ok("Hello World!");
    }

}