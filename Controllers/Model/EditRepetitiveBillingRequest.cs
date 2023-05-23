using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class EditRepetitiveBillingRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    [Required]
    public RepetitiveBillingId RepetitiveBillingId { get; set; } = new RepetitiveBillingId();
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public FrequenceMode FrequenceMode { get; set; }
}