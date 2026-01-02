using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace cashflow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpensesUseCase useCase,
        [FromBody] RequestRegisterExepenseJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
