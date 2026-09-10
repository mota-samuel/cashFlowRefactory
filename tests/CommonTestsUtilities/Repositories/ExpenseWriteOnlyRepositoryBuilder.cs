using Cashflow.Domain.Repositories.Expense;
using Moq;

namespace CommonTestsUtilities.Repositories;
public class ExpenseWriteOnlyRepositoryBuilder
{
    public static IExpensesWriteRepository Build()
    {
        var mock = new Mock<IExpensesWriteRepository>();
        
        return mock.Object;
    }
}
