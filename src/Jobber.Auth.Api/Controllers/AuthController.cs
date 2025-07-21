using Api.Options;
using Api.Services;
using Jobber.Auth.Application.Auth.LoginUser;
using Jobber.Auth.Application.Auth.Logout;
using Jobber.Auth.Application.Auth.RegisterUser;
using Jobber.Auth.Application.Auth.UpdateAuthTokens;
using Jobber.Jwt.Options;
using Jobber.Jwt.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator, 
    IOptionsSnapshot<RefreshTokenCookieOptions> cookieOptions, 
    ICookieService cookieService,
    IOptionsSnapshot<AccessTokenCookieOptions> accessTokenCookieOptions)
    : ControllerBase
{
    private readonly ICookieService _cookieService = cookieService;
    private readonly IMediator _mediator = mediator;
    private readonly RefreshTokenCookieOptions _refreshTokenCookieOptions = cookieOptions.Value;
    private readonly AccessTokenCookieOptions _accessTokenCookieOptions = accessTokenCookieOptions.Value;

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var tokens = await _mediator.Send(command, cancellationToken);
        _cookieService.AppendCookieAuthTokens(Response, tokens);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        var tokens = await _mediator.Send(command, cancellationToken);
        _cookieService.AppendCookieAuthTokens(Response, tokens);
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
        var accessTokenCookieName = _accessTokenCookieOptions.AccessJwtTokenCookieName;
        var refreshTokenCookieName = _refreshTokenCookieOptions.RefreshTokenCookieName;
        
        var refreshToken = Request.Cookies[refreshTokenCookieName];
        if (refreshToken is not null)
        {
            var command = new LogoutCommand
            {
                RefreshToken = refreshToken
            };
            await _mediator.Send(command, cancellationToken);
            Response.Cookies.Delete(refreshTokenCookieName);
        }
        Response.Cookies.Delete(accessTokenCookieName);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshTokenCookieName = _refreshTokenCookieOptions.RefreshTokenCookieName;
        var command = new UpdateAuthTokensCommand
        {
            RefreshToken = Request.Cookies[refreshTokenCookieName],
        };
        var tokens = await _mediator.Send(command, cancellationToken);
        _cookieService.AppendCookieAuthTokens(Response, tokens);
        return Ok();
    }
}