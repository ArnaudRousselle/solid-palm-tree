using System.ComponentModel.DataAnnotations;
using MyPersonalAccounting.ActorSystem.Portfolio;

namespace MyPersonalAccounting.Controllers.Model;

public class AddBillingRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public bool Checked { get; set; }
    [Required(AllowEmptyStrings = true)]
    public string Comment { get; set; } = "";
    [Required]
    public bool IsArchived { get; set; }
    [Required]
    public bool IsSaving { get; set; }
}