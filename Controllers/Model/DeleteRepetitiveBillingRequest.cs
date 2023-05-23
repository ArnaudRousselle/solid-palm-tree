using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class DeleteRepetitiveBillingRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    [Required]
    public RepetitiveBillingId RepetitiveBillingId { get; set; } = new RepetitiveBillingId();
}