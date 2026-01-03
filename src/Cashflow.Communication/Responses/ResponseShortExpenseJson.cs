namespace Cashflow.Communication.Responses;
public class ResponseShortExpenseJson
{
    public Guid ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
