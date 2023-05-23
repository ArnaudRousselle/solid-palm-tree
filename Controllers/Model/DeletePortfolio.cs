using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class DeletePortfolioRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
}