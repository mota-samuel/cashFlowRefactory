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
        switch (context.Exception)
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

    /*
        private void HandleProjectException(ExceptionContext context)
        {

            switch



            if(context.Exception is ErrorOnValidationException)
            {
                var cast = context.Exception as ErrorOnValidationException;

                var errorResponse = new ResponseErrorJson(cast.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        private void ThrowUnknownException(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(errorResponse);
        }*/
}
