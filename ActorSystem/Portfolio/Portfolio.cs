using System.Collections.Immutable;
using System.Text.Json.Serialization;
using MyPersonalAccounting.Converters;
using Proto;

namespace MyPersonalAccounting.ActorSystem.Portfolio;

public class Portfolio : WithSnapshotActor<PortfolioState>
{
    public Portfolio(PidInformations pidInformations, IStore<PortfolioState> store) : base(pidInformations, store)
    {
    }

    protected override Task<IEvent?> HandleAsync(IContext context)
    {
        switch (context.Message)
        {
            case AddBilling msg:
                return Handle(context, msg);
            case AddRepetitiveBilling msg:
                return Handle(context, msg);
            case CreatePortfolio msg:
                return Handle(context, msg);
            case DeleteBilling msg:
                return Handle(context, msg);
            case DeleteRepetitiveBilling msg:
                return Handle(context, msg);
            case EditBilling msg:
                return Handle(context, msg);
            case EditPortfolio msg:
                return Handle(context, msg);
            case EditRepetitiveBilling msg:
                return Handle(context, msg);
            case InsertNextBilling msg:
                return Handle(context, msg);
            case MarkAsChecked msg:
                return Handle(context, msg);
            case Started msg:
                return Handle(context, msg);
        }

        return NoEvent();
    }

    private Task<IEvent?> Handle(IContext context, AddBilling msg)
    {
        var billingState = new PortfolioState.BillingState(
            new BillingId(),
            msg.Date,
            msg.Label,
            msg.Amount,
            msg.Checked,
            msg.Comment,
            msg.IsArchived,
            msg.IsSaving);

        State = State with { Billings = State.Billings.Add(billingState) };

        return Event(new BillingAdded(State.PortfolioId, billingState));
    }

    private Task<IEvent?> Handle(IContext context, AddRepetitiveBilling msg)
    {
        var repetitiveBillingState = new PortfolioState.RepetitiveBillingState(
            new RepetitiveBillingId(),
            msg.Date,
            msg.Label,
            msg.Amount,
            msg.Frequence);

        State = State with { RepetitiveBillings = State.RepetitiveBillings.Add(repetitiveBillingState) };

        return Event(new RepetitiveBillingAdded(State.PortfolioId, repetitiveBillingState));
    }

    private Task<IEvent?> Handle(IContext context, CreatePortfolio msg)
    {
        State = State with { Name = msg.Name };
        return Event(new PortfolioCreated(State.PortfolioId, msg.Name));
    }

    private Task<IEvent?> Handle(IContext context, DeleteBilling msg)
    {
        var billingState = State.Billings.FirstOrDefault(n => n.BillingId == msg.BillingId);

        if (billingState == null)
            return NoEvent();

        State = State with { Billings = State.Billings.Remove(billingState) };

        return Event(new BillingDeleted(State.PortfolioId, billingState));
    }

    private Task<IEvent?> Handle(IContext context, DeleteRepetitiveBilling msg)
    {
        var repetitiveBillingState = State.RepetitiveBillings.FirstOrDefault(n => n.RepetitiveBillingId == msg.RepetitiveBillingId);

        if (repetitiveBillingState == null)
            return NoEvent();

        State = State with { RepetitiveBillings = State.RepetitiveBillings.Remove(repetitiveBillingState) };

        return Event(new RepetitiveBillingDeleted(State.PortfolioId, repetitiveBillingState));
    }

    private Task<IEvent?> Handle(IContext context, EditBilling msg)
    {
        var previousBillingState = State.Billings.FirstOrDefault(n => n.BillingId == msg.BillingId);

        if (previousBillingState == null)
            return NoEvent();

        var newBillingState = previousBillingState with
        {
            Date = msg.Date,
            Label = msg.Label,
            Amount = msg.Amount,
            Checked = msg.Checked,
            Comment = msg.Comment,
            IsArchived = msg.IsArchived,
            IsSaving = msg.IsSaving
        };

        State = State with
        {
            Billings = State.Billings
                .Remove(previousBillingState)
                .Add(newBillingState)
        };

        return Event(new BillingEdited(State.PortfolioId, newBillingState));
    }

    private Task<IEvent?> Handle(IContext context, EditPortfolio msg)
    {
        State = State with { Name = msg.Name };
        return Event(new PortfolioEdited(State.PortfolioId, msg.Name));
    }

    private Task<IEvent?> Handle(IContext context, EditRepetitiveBilling msg)
    {
        var previousRepetitiveBillingState = State.RepetitiveBillings.FirstOrDefault(n => n.RepetitiveBillingId == msg.RepetitiveBillingId);

        if (previousRepetitiveBillingState == null)
            return NoEvent();

        var newRepetitiveBillingState = new PortfolioState.RepetitiveBillingState(
            msg.RepetitiveBillingId,
            msg.Date,
            msg.Label,
            msg.Amount,
            msg.Frequence);

        State = State with
        {
            RepetitiveBillings = State.RepetitiveBillings
                .Remove(previousRepetitiveBillingState)
                .Add(newRepetitiveBillingState)
        };

        return Event(new RepetitiveBillingEdited(State.PortfolioId, newRepetitiveBillingState));
    }

    private Task<IEvent?> Handle(IContext context, InsertNextBilling msg)
    {
        var previousRepetitiveBillingState = State.RepetitiveBillings.FirstOrDefault(n => n.RepetitiveBillingId == msg.RepetitiveBillingId);

        if (previousRepetitiveBillingState == null)
            return NoEvent();

        var billingState = new PortfolioState.BillingState(
            new BillingId(),
            previousRepetitiveBillingState.Date,
            previousRepetitiveBillingState.Label,
            previousRepetitiveBillingState.Amount,
            false,
            "",
            false,
            false);

        var shift = previousRepetitiveBillingState.Frequence switch
        {
            FrequenceMode.Annual => 12,
            FrequenceMode.Quarterly => 3,
            FrequenceMode.Bimonthly => 2,
            FrequenceMode.Monthly => 1,
            _ => 0
        };

        var editedRepetitiveBillingState = previousRepetitiveBillingState with
        {
            Date = previousRepetitiveBillingState.Date.AddMonths(shift)
        };

        State = State with
        {
            Billings = State.Billings.Add(billingState),
            RepetitiveBillings = State.RepetitiveBillings
                .Remove(previousRepetitiveBillingState)
                .Add(editedRepetitiveBillingState)
        };

        return Event(new NextBillingInserted(State.PortfolioId, billingState, editedRepetitiveBillingState));
    }

    private Task<IEvent?> Handle(IContext context, MarkAsChecked msg)
    {
        var previousBillingState = State.Billings.FirstOrDefault(n => n.BillingId == msg.BillingId);

        if (previousBillingState == null)
            return NoEvent();

        var newBillingState = previousBillingState with
        {
            Checked = msg.Checked,
        };

        State = State with
        {
            Billings = State.Billings
                .Remove(previousBillingState)
                .Add(newBillingState)
        };

        return Event(new BillingMarkedAsChecked(State.PortfolioId, newBillingState));
    }

    private Task<IEvent?> Handle(IContext context, Started msg)
    {
        var str = context.Self.ToString();
        var index = str.IndexOf("portfolio-") + 10;

        State = State with
        {
            PortfolioId = new PortfolioId(str.Substring(index))
        };

        return Event(new PortfolioInitialized(State.PortfolioId, State));
    }
}



public record PortfolioState
{
    public PortfolioId PortfolioId { get; init; } = new PortfolioId();
    public record BillingState(BillingId BillingId, DateOnly Date, string Label, decimal Amount, bool Checked, string Comment, bool IsArchived, bool IsSaving);
    public record RepetitiveBillingState(RepetitiveBillingId RepetitiveBillingId, DateOnly Date, string Label, decimal Amount, FrequenceMode Frequence);
    public string Name { get; init; } = "";
    public ImmutableArray<BillingState> Billings { get; init; } = ImmutableArray.Create<BillingState>();
    public ImmutableArray<RepetitiveBillingState> RepetitiveBillings { get; init; } = ImmutableArray.Create<RepetitiveBillingState>();
}

[JsonConverter(typeof(ItemIdentityConverter<PortfolioId>))]
public record PortfolioId : ItemIdentity
{
    public PortfolioId() : base()
    {
    }

    public PortfolioId(Guid guid) : base(guid)
    {
    }

    public PortfolioId(string guidStr) : base(guidStr)
    {
    }
}