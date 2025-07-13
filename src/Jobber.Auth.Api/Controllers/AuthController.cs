using Jobber.Auth.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var jwtToken = await _mediator.Send(command, cancellationToken);
        Response.Cookies.Append("token", jwtToken);
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
    public IActionResult Test(CancellationToken cancellationToken)
    {
        return Ok("Hello World!");
    }

}