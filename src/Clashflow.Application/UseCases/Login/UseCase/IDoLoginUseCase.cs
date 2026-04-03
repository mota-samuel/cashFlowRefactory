using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.Login.UseCase;
public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
