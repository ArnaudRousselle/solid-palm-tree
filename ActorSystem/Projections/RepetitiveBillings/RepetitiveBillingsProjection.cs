using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.Hubs;
using Proto;

namespace MyPersonalAccounting.ActorSystem.Projections.RepetitiveBillings;

public class RepetitiveBillingsProjection : ProjectionActor
{
    private readonly Dictionary<PortfolioId, SortedSet<RepetitiveBillingItem>> _portfolios = new();

    public RepetitiveBillingsProjection(IHubContext<ProjectionsHub, IProjectionsClient> hubContext)
        : base(hubContext) { }

    public override async Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case GetRepetitiveBillings getRepetitiveBillingsRequest:
                {
                    _portfolios.TryGetValue(getRepetitiveBillingsRequest.PortfolioId, out SortedSet<RepetitiveBillingItem>? repetitiveBillings);
                    Respond(context, getRepetitiveBillingsRequest, repetitiveBillings?.ToArray() ?? new RepetitiveBillingItem[0]);
                    return;
                }
            case RepetitiveBillingAdded repetitiveBillingAddedEvent:
                {
                    if (!_portfolios.TryGetValue(repetitiveBillingAddedEvent.PortfolioId, out SortedSet<RepetitiveBillingItem>? repetitiveBillings))
                        return;

                    repetitiveBillings.Add(new RepetitiveBillingItem(repetitiveBillingAddedEvent.NewRepetitiveBillingState));

                    await Notify(new RepetitiveBillingsProjectionArgs(repetitiveBillingAddedEvent.PortfolioId));
                    break;
                }
            case RepetitiveBillingDeleted repetitiveBillingDeletedEvent:
                {
                    if (!_portfolios.TryGetValue(repetitiveBillingDeletedEvent.PortfolioId, out SortedSet<RepetitiveBillingItem>? repetitiveBillings))
                        return;

                    var previous = repetitiveBillings.FirstOrDefault(b => b.RepetitiveBillingId == repetitiveBillingDeletedEvent.RemovedRepetitiveBillingState.RepetitiveBillingId);

                    if (previous == null)
                        return;

                    repetitiveBillings.Remove(previous);

                    await Notify(new RepetitiveBillingsProjectionArgs(repetitiveBillingDeletedEvent.PortfolioId));
                    break;
                }
            case RepetitiveBillingEdited repetitiveBillingEditedEvent:
                {
                    if (!_portfolios.TryGetValue(repetitiveBillingEditedEvent.PortfolioId, out SortedSet<RepetitiveBillingItem>? repetitiveBillings))
                        return;

                    var previous = repetitiveBillings.FirstOrDefault(b => b.RepetitiveBillingId == repetitiveBillingEditedEvent.EditedRepetitiveBillingState.RepetitiveBillingId);

                    if (previous == null)
                        return;

                    repetitiveBillings.Remove(previous);
                    repetitiveBillings.Add(new RepetitiveBillingItem(repetitiveBillingEditedEvent.EditedRepetitiveBillingState));

                    await Notify(new RepetitiveBillingsProjectionArgs(repetitiveBillingEditedEvent.PortfolioId));
                    break;
                }
            case PortfolioCreated portfolioCreatedEvent:
                {
                    if (_portfolios.ContainsKey(portfolioCreatedEvent.PortfolioId))
                        return;

                    _portfolios[portfolioCreatedEvent.PortfolioId] = new SortedSet<RepetitiveBillingItem>(new RepetitiveBillingItemComparer());

                    await Notify(new RepetitiveBillingsProjectionArgs(portfolioCreatedEvent.PortfolioId));
                    break;
                }
            case PortfolioInitialized portfolioInitializedEvent:
                {
                    if (_portfolios.ContainsKey(portfolioInitializedEvent.PortfolioId))
                        return;

                    var sortedSet = new SortedSet<RepetitiveBillingItem>(new RepetitiveBillingItemComparer());

                    _portfolios[portfolioInitializedEvent.PortfolioId] = sortedSet;

                    foreach (var rb in portfolioInitializedEvent.State.RepetitiveBillings)
                        sortedSet.Add(new RepetitiveBillingItem(rb));

                    await Notify(new RepetitiveBillingsProjectionArgs(portfolioInitializedEvent.PortfolioId));
                    break;
                }
            case NextBillingInserted nextBillingInsertedEvent:
                {
                    if (!_portfolios.TryGetValue(nextBillingInsertedEvent.PortfolioId, out SortedSet<RepetitiveBillingItem>? repetitiveBillings))
                        return;

                    var previous = repetitiveBillings.FirstOrDefault(b => b.RepetitiveBillingId == nextBillingInsertedEvent.EditedRepetitiveBillingState.RepetitiveBillingId);

                    if (previous == null)
                        return;

                    repetitiveBillings.Remove(previous);
                    repetitiveBillings.Add(new RepetitiveBillingItem(nextBillingInsertedEvent.EditedRepetitiveBillingState));

                    await Notify(new RepetitiveBillingsProjectionArgs(nextBillingInsertedEvent.PortfolioId));
                    break;
                }
        }
    }
}

public class RepetitiveBillingsProjectionArgs : ProjectionArgs
{
    [Required]
    public PortfolioId PortfolioId { get; set; }

    public RepetitiveBillingsProjectionArgs(PortfolioId portfolioId)
    {
        PortfolioId = portfolioId;
    }
}

public class RepetitiveBillingItem
{
    [Required]
    public RepetitiveBillingId RepetitiveBillingId { get; internal set; }
    [Required]
    public DateOnly Date { get; internal set; }
    [Required]
    public string Label { get; internal set; }
    [Required]
    public decimal Amount { get; internal set; }
    [Required]
    public FrequenceMode Frequence { get; internal set; }

    public RepetitiveBillingItem(PortfolioState.RepetitiveBillingState state)
    {
        RepetitiveBillingId = state.RepetitiveBillingId;
        Date = state.Date;
        Label = state.Label;
        Amount = state.Amount;
        Frequence = state.Frequence;
    }
}

public class RepetitiveBillingItemComparer : IComparer<RepetitiveBillingItem>
{
    public int Compare(RepetitiveBillingItem? x, RepetitiveBillingItem? y)
    {
        if (x == null && y == null)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        int compare;

        if ((compare = x.Date.CompareTo(y.Date)) == 0
            && (compare = x.Amount.CompareTo(y.Amount)) == 0)
            return x.RepetitiveBillingId.Guid.CompareTo(y.RepetitiveBillingId.Guid);

        return compare;
    }
}
