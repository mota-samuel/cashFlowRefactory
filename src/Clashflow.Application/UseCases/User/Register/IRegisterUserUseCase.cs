using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;

namespace Cashflow.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
