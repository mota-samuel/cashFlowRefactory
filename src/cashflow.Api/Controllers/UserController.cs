using Cashflow.Application.UseCases.User.Register;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace cashflow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);
    
            return Created(string.Empty, response);
    }
}
