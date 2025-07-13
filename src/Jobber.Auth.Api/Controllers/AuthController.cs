using Api.Options;
using Jobber.Auth.Application.Auth.LoginUser;
using Jobber.Auth.Application.Auth.RegisterUser;
using Jobber.Auth.Application.Auth.UpdateAuthTokens;
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

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var tokens = await _mediator.Send(command, cancellationToken);
        Response.Cookies.Append(_apiCookieOptions.AccessTokenCookieName, tokens.AccessToken);
        Response.Cookies.Append(_apiCookieOptions.RefreshTokenCookieName, tokens.RefreshToken.Token);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        var tokens = await _mediator.Send(command, cancellationToken);
        Response.Cookies.Append(_apiCookieOptions.AccessTokenCookieName, tokens.AccessToken);
        Response.Cookies.Append(_apiCookieOptions.RefreshTokenCookieName, tokens.RefreshToken.Token);
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

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var command = new UpdateAuthTokensCommand
        {
            // ! because there's middleware check for null
            RefreshToken = Request.Cookies[_apiCookieOptions.RefreshTokenCookieName]!,
        };
        var tokens = await _mediator.Send(command, cancellationToken);
        Response.Cookies.Append(_apiCookieOptions.AccessTokenCookieName, tokens.AccessToken);
        Response.Cookies.Append(_apiCookieOptions.RefreshTokenCookieName, tokens.RefreshToken.Token);
        return Ok();
    }

}