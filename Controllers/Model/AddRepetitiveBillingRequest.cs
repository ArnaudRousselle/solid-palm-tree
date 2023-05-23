using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class AddRepetitiveBillingRequest
{
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    public DateOnly Date { get; set; }
    public string Name { get; set; } = "";
    public decimal Amount { get; set; }
    public FrequenceMode FrequenceMode { get; set; }
}