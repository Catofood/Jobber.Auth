using Api.Extensions;
using Api.Options;
using Jobber.Auth.Application.Auth.LoginUser;
using Jobber.Auth.Application.Auth.Logout;
using Jobber.Auth.Application.Auth.RegisterUser;
using Jobber.Auth.Application.Auth.UpdateAuthTokens;
using MediatR;
using Microsoft.AspNetCore.Authentication;
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
        Response.AppendCookieAuthTokens(_apiCookieOptions, tokens);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        var tokens = await _mediator.Send(command, cancellationToken);
        Response.AppendCookieAuthTokens(_apiCookieOptions, tokens);
        return Ok();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] Guid userId, 
        [FromQuery] string confirmationToken, 
        CancellationToken cancellationToken)
    {
        // TODO: Реализовать систему подтверждения почты через SMTP сервис
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
        var refreshToken = Request.Cookies[_apiCookieOptions.RefreshTokenCookieName];
        if (refreshToken is not null)
        {
            var command = new LogoutCommand
            {
                RefreshToken = refreshToken
            };
            await _mediator.Send(command, cancellationToken);
            Response.Cookies.Delete(_apiCookieOptions.RefreshTokenCookieName);
        }
        Response.Cookies.Delete(_apiCookieOptions.AccessTokenCookieName);
        return Ok();
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
        Response.AppendCookieAuthTokens(_apiCookieOptions, tokens);
        return Ok();
    }
}