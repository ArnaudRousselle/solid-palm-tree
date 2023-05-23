using MyPersonalAccounting.ActorSystem.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class EditBillingRequest
{
    [Required]
    public PortfolioId PortfolioId { get; set; } = new PortfolioId();
    [Required]
    public BillingId BillingId { get; set; } = new BillingId();
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