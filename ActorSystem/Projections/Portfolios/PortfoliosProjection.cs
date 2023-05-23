using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.Hubs;
using Proto;

namespace MyPersonalAccounting.ActorSystem.Projections.Portfolios;

public class PortfoliosProjection : ProjectionActor
{
    private readonly SortedSet<PortfolioItem> _portfolios = new(new PortfolioItemComparer());

    public PortfoliosProjection(IHubContext<ProjectionsHub, IProjectionsClient> hubContext)
        : base(hubContext) { }

    public override async Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case GetPortfolios getPortfoliosRequest:
                {
                    Respond(context, getPortfoliosRequest, _portfolios.ToArray());
                    return;
                }
            case PortfolioCreated portfolioCreatedEvent:
                {
                    var previous = _portfolios.FirstOrDefault(b => b.PortfolioId == portfolioCreatedEvent.PortfolioId);

                    if (previous == null)
                        return;

                    var portfolioItem = new PortfolioItem(portfolioCreatedEvent.PortfolioId)
                    {
                        Name = portfolioCreatedEvent.Name
                    };

                    _portfolios.Remove(previous);
                    _portfolios.Add(portfolioItem);

                    await Notify(new PortfoliosProjectionArgs());
                    break;
                }
            case PortfolioEdited portfolioEditedEvent:
                {
                    var previous = _portfolios.FirstOrDefault(b => b.PortfolioId == portfolioEditedEvent.PortfolioId);

                    if (previous == null)
                        return;

                    var portfolioItem = new PortfolioItem(portfolioEditedEvent.PortfolioId)
                    {
                        Name = portfolioEditedEvent.Name
                    };

                    _portfolios.Remove(previous);
                    _portfolios.Add(portfolioItem);

                    await Notify(new PortfoliosProjectionArgs());
                    break;
                }
            case PortfolioInitialized portfolioInitializedEvent:
                {
                    var previous = _portfolios.FirstOrDefault(b => b.PortfolioId == portfolioInitializedEvent.PortfolioId);

                    if (previous != null)
                        return;

                    var portfolioItem = new PortfolioItem(portfolioInitializedEvent.PortfolioId)
                    {
                        Name = portfolioInitializedEvent.State.Name
                    };

                    _portfolios.Add(portfolioItem);
                    await Notify(new PortfoliosProjectionArgs());
                    break;
                }
        }
    }
}

public class PortfoliosProjectionArgs : ProjectionArgs
{
}

public class PortfolioItem
{
    [Required]
    public PortfolioId PortfolioId { get; internal set; }
    [Required]
    public string Name { get; internal set; } = "";
    public PortfolioItem(PortfolioId portfolioId)
    {
        PortfolioId = portfolioId;
    }
}

public class PortfolioItemComparer : IComparer<PortfolioItem>
{
    public int Compare(PortfolioItem? x, PortfolioItem? y)
    {
        if (x == null && y == null)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        int compare;

        if ((compare = x.Name.CompareTo(y.Name)) == 0)
            return x.PortfolioId.Guid.CompareTo(y.PortfolioId.Guid);

        return compare;
    }
}
