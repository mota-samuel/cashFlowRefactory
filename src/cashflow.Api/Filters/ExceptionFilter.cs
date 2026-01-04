using Cashflow.Communication.Responses;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace cashflow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashflowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }

    }

    private void HandleProjectException(ExceptionContext context)
    {
        //com o cast declarado desta forma, se a excecao nao for um cashflowException, ira acontecer uma excecao, se declarar usando a palavra reservada `as` e a '!' o cod executa como nulo
        var cashflowException = (CashflowException)context.Exception;
        context.HttpContext.Response.StatusCode = cashflowException.StatusCode;
        context.Result = new ObjectResult(cashflowException.GetErrors());
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(errorResponse);
    }
}

/*No switch abaixo eu havia feito uma refatoracao no codigo do projeto, no cod acima foi refatorado para abstrair o filtro de forma a nao ter que ficar modificando o cod para incluir novos filtros, aplicando o conceito Open-Close do SOLID
 * 
 * switch (context.Exception)
        {
            case ErrorOnValidationException ex:
                var errorResponseJSon = new ResponseErrorJson(ex.Errors);
                context.Result = new BadRequestObjectResult(errorResponseJSon);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            case NotFoundException ex:
                context.Result = new NotFoundObjectResult(ex.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

            default:
                var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

                context.Result = new ObjectResult(errorResponse);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                break;
        }

    }

    //eu isolei todo esse contexto de código para simplificar toda essa execucao em um unico switch, acredito que seja mais facil a interpretacao do cod e agilize a execucao

    
       

       */