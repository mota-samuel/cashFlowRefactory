using Cashflow.Application.UseCases.Expenses.Delete;
using Cashflow.Application.UseCases.Expenses.GetAll;
using Cashflow.Application.UseCases.Expenses.GetById;
using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Application.UseCases.Expenses.Update;
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
    [ProducesResponseType(typeof(ResponsResgisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorOnValidationException), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpensesUseCase useCase,
        [FromBody] RequestExepenseJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllItems([FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.AllExpenses.Count != 0)
            return Ok(response);
        
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
                                                [FromServices] IGetExpenseByIdUseCase useCase, 
                                                 Guid id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(
                                                [FromServices] IDeleteExpenseUseCase useCase, 
                                                 Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }


    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateExpense(
                                                [FromServices] IUpdateExpenseUseCase useCase, 
                                                 Guid id,
                                                 [FromBody]RequestExepenseJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
 