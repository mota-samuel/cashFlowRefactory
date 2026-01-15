using Bogus;
using Cashflow.Communication.Enums;
using Cashflow.Communication.Requests;

namespace CommonTestsUtilities.Requests;
public class RequestRegisterExepenseJsonBuilder
{
    //A classe Builder serve para facilitar a criação de objetos complexos em testes, ela foi definida como static para permitir o acesso direto aos seus métodos sem a necessidade de instanciar a classe.
    public static RequestExepenseJson Build()
    {
       /* var faker = new Faker();

       var request = new RequestRegisterExepenseJson
        {
            Title = faker.Commerce.ProductName(),
            Description = faker.Lorem.Sentence(),
            Date = faker.Date.Past(1),
            Amount = faker.Finance.Amount(10, 1000),
            PaymentType = faker.PickRandom<Cashflow.Communication.Enums.PaymentType>()
        };*/

        //Esse metodo alternativo usando RuleFor, uma forma mais fluente de criar o objeto
        var request = new Faker<RequestExepenseJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Lorem.Sentence())
            .RuleFor(r => r.Date, f => f.Date.Past(1))
            .RuleFor(r => r.Amount, f => f.Random.Decimal(min:1, max:100))
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>())
            .Generate();

        return request;
    }
}
