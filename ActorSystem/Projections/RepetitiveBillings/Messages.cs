using MyPersonalAccounting.ActorSystem.Portfolio;

namespace MyPersonalAccounting.ActorSystem.Projections.RepetitiveBillings;

public class GetRepetitiveBillings : IRequest<RepetitiveBillingItem[]>
{
    public PortfolioId PortfolioId { get; set; }

    public GetRepetitiveBillings(PortfolioId portfolioId)
    {
        PortfolioId = portfolioId;
    }
}