using Cashflow.Application.UseCases.Expenses.Report.Month.Excel;
using Cashflow.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;

namespace cashflow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.ADMIN)]
public class ReportController : ControllerBase
{

    [HttpGet("ExcelReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly requestDateFilter,
        [FromServices] IGenerateExpensesReportExcelUseCase useCase)
    {

        byte[] file = await useCase.Execute(requestDateFilter);
        DateTime date = DateTime.UtcNow; 
        string title = $"{date}_report.xlsx" ;

        if(file.Length != 0)
        return File(file, MediaTypeNames.Application.Octet,title);

        return NoContent();
    }
}
