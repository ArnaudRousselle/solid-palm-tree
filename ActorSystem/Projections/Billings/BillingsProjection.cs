using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.Hubs;
using Proto;

namespace MyPersonalAccounting.ActorSystem.Projections.Billings;

public class BillingsProjection : ProjectionActor
{
    private readonly Dictionary<PortfolioId, SortedSet<BillingItem>> _portfolios = new();

    public BillingsProjection(IHubContext<ProjectionsHub, IProjectionsClient> hubContext)
        : base(hubContext) { }

    public override async Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case BillingAdded billingAddedEvent:
                {
                    if (!_portfolios.TryGetValue(billingAddedEvent.PortfolioId, out SortedSet<BillingItem>? billings))
                        return;

                    billings.Add(new BillingItem(billingAddedEvent.NewBillingState));

                    await Notify(new BillingsProjectionArgs(billingAddedEvent.PortfolioId));
                    break;
                }
            case BillingDeleted billingDeletedEvent:
                {
                    if (!_portfolios.TryGetValue(billingDeletedEvent.PortfolioId, out SortedSet<BillingItem>? billings))
                        return;

                    var previous = billings.FirstOrDefault(b => b.BillingId == billingDeletedEvent.RemovedBillingState.BillingId);

                    if (previous == null)
                        return;

                    billings.Remove(previous);

                    await Notify(new BillingsProjectionArgs(billingDeletedEvent.PortfolioId));
                    break;
                }
            case BillingEdited billingEditedEvent:
                {
                    if (!_portfolios.TryGetValue(billingEditedEvent.PortfolioId, out SortedSet<BillingItem>? billings))
                        return;

                    var billing = new BillingItem(billingEditedEvent.EditedBillingState);

                    var previous = billings.FirstOrDefault(b => b.BillingId == billing.BillingId);

                    if (previous == null)
                        return;

                    billings.Remove(previous);
                    billings.Add(billing);

                    await Notify(new BillingsProjectionArgs(billingEditedEvent.PortfolioId));
                    break;
                }
            case BillingMarkedAsChecked billingMarkedAsCheckedEvent:
                {
                    if (!_portfolios.TryGetValue(billingMarkedAsCheckedEvent.PortfolioId, out SortedSet<BillingItem>? billings))
                        return;

                    var billing = new BillingItem(billingMarkedAsCheckedEvent.EditedBillingState);

                    var previous = billings.FirstOrDefault(b => b.BillingId == billing.BillingId);

                    if (previous == null)
                        return;

                    billings.Remove(previous);
                    billings.Add(billing);

                    await Notify(new BillingsProjectionArgs(billingMarkedAsCheckedEvent.PortfolioId));
                    break;
                }
            case GetBillings getBillingsRequest:
                {
                    _portfolios.TryGetValue(getBillingsRequest.PortfolioId, out SortedSet<BillingItem>? billings);
                    Respond(context, getBillingsRequest, billings?.ToArray() ?? new BillingItem[0]);
                    return;
                }
            case PortfolioCreated portfolioCreatedEvent:
                {
                    if (_portfolios.ContainsKey(portfolioCreatedEvent.PortfolioId))
                        return;

                    _portfolios[portfolioCreatedEvent.PortfolioId] = new SortedSet<BillingItem>(new BillingItemComparer());

                    await Notify(new BillingsProjectionArgs(portfolioCreatedEvent.PortfolioId));
                    break;
                }
            case PortfolioInitialized portfolioInitializedEvent:
                {
                    if (_portfolios.ContainsKey(portfolioInitializedEvent.PortfolioId))
                        return;

                    var sortedSet = new SortedSet<BillingItem>(new BillingItemComparer());

                    _portfolios[portfolioInitializedEvent.PortfolioId] = sortedSet;

                    foreach (var b in portfolioInitializedEvent.State.Billings)
                        sortedSet.Add(new BillingItem(b));

                    await Notify(new BillingsProjectionArgs(portfolioInitializedEvent.PortfolioId));
                    break;
                }
            case NextBillingInserted nextBillingInsertedEvent:
                {
                    if (!_portfolios.TryGetValue(nextBillingInsertedEvent.PortfolioId, out SortedSet<BillingItem>? billings))
                        return;

                    billings.Add(new BillingItem(nextBillingInsertedEvent.NewBillingState));
                    await Notify(new BillingsProjectionArgs(nextBillingInsertedEvent.PortfolioId));
                    break;
                }
        }
    }
}

public class BillingsProjectionArgs : ProjectionArgs
{
    [Required]
    public PortfolioId PortfolioId { get; set; }

    public BillingsProjectionArgs(PortfolioId portfolioId)
    {
        PortfolioId = portfolioId;
    }
}

public class BillingItem
{
    [Required]
    public BillingId BillingId { get; internal set; }
    [Required]
    public DateOnly Date { get; internal set; }
    [Required]
    public string Label { get; internal set; }
    [Required]
    public decimal Amount { get; internal set; }
    [Required]
    public bool Checked { get; internal set; }
    [Required]
    public string Comment { get; internal set; }
    [Required]
    public bool IsArchived { get; internal set; }
    [Required]
    public bool IsSaving { get; internal set; }

    public BillingItem(PortfolioState.BillingState state)
    {
        BillingId = state.BillingId;
        Date = state.Date;
        Label = state.Label;
        Amount = state.Amount;
        Checked = state.Checked;
        Comment = state.Comment;
        IsArchived = state.IsArchived;
        IsSaving = state.IsSaving;
    }
}

class BillingItemComparer : IComparer<BillingItem>
{
    public int Compare(BillingItem? x, BillingItem? y)
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
            return x.BillingId.Guid.CompareTo(y.BillingId.Guid);

        return compare;
    }
}