using System.ComponentModel.DataAnnotations;

namespace MyPersonalAccounting.Controllers.Model;

public class CreatePortfolioRequest
{
    [Required]
    public string Name { get; set; } = "";
}