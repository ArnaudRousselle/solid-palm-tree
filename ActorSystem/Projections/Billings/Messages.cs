using MyPersonalAccounting.ActorSystem.Portfolio;

namespace MyPersonalAccounting.ActorSystem.Projections.Billings;

public class GetBillings : IRequest<BillingItem[]>
{
    public PortfolioId PortfolioId { get; set; }

    public GetBillings(PortfolioId portfolioId)
    {
        PortfolioId = portfolioId;
    }
}