using AutoMapper;
using Cashflow.Communication.Requests;
using Cashflow.Communication.Responses;
using Cashflow.Domain.Entities;

namespace Cashflow.Application.Mapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExepenseJson, Expense>();
    }
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponsResgisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
    }
}
