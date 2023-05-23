using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class DeleteBillingRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    [Required]
    public BillingId BillingId { get; set; } = new BillingId();
}