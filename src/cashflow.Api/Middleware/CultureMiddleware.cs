using System.Globalization;

namespace cashflow.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        //recebo o idioma encaminhado na requisição
        var cultureQuery = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        //define o idioma defualt
        var cultureInfo = new CultureInfo("en-US");
        //verifico se foi encaminhado algum idioma na requisição, se sim, altero o idioma default para o encaminhado
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            cultureInfo = new CultureInfo(cultureQuery);
        }
        //defino o idioma da aplicação
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        //continua o processamento da requisição
        await _next(context);
    }

}
