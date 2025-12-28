using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace cashflow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExepenseJson request)
    {
        var useCase = new RegisterExpensesUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
