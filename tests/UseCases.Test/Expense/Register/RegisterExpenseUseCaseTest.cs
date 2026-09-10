using Cashflow.Application.UseCases.Expenses.Register;
using Cashflow.Domain.Entities;
using Cashflow.Exception;
using Cashflow.Exception.ExceptionBase;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Expense.Register;
public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterExepenseJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task FailTitleEmpty()
    {
        var loggedUser = UserBuilder.Build();

        var request = RequestRegisterExepenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(error => error.GetErrors().Count() == 1 && error.GetErrors().Contains(ResourceErrorMessages.REQUIRED_TITLE));
    }

    private RegisterExpensesUseCase CreateUseCase(Cashflow.Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilde.Build();
        var repository = ExpenseWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterExpensesUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}
