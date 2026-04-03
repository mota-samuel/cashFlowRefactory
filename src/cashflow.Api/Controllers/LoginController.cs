using Cashflow.Application.UseCases.Login;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace cashflow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
