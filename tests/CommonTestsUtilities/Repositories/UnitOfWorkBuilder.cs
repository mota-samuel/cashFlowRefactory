using Cashflow.Domain.Repositories;
using Moq;

namespace CommonTestsUtilities.Repositories;
public class UnitOfWorkBuilde
{
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

        return mock.Object;
         }
}
