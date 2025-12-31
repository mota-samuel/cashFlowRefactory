using Cashflow.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cashflow.Domain.Entities;
public class Expense
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    public required DateTime Date { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public required decimal Amount { get; set; }

    public required PaymentType PaymentType { get; set; }
}

