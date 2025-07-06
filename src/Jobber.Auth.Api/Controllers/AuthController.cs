using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        throw new NotImplementedException();
    }
}