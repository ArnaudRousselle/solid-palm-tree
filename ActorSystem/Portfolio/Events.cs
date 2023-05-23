namespace MyPersonalAccounting.ActorSystem.Portfolio;

public abstract class PortfolioEvent : IEvent
{
    public ItemIdentity ActorId { get; }
    public PortfolioId PortfolioId => (PortfolioId)ActorId;

    protected PortfolioEvent(ItemIdentity actorId)
    {
        ActorId = actorId;
    }
}

public class BillingAdded : PortfolioEvent
{
    public PortfolioState.BillingState NewBillingState { get; }

    public BillingAdded(ItemIdentity actorId, PortfolioState.BillingState newBillingState)
        : base(actorId)
    {
        NewBillingState = newBillingState;
    }
}

public class BillingDeleted : PortfolioEvent
{
    public PortfolioState.BillingState RemovedBillingState { get; }

    public BillingDeleted(ItemIdentity actorId, PortfolioState.BillingState removedBillingState)
        : base(actorId)
    {
        RemovedBillingState = removedBillingState;
    }
}

public class BillingEdited : PortfolioEvent
{
    public PortfolioState.BillingState EditedBillingState { get; }

    public BillingEdited(ItemIdentity actorId, PortfolioState.BillingState editedBillingState)
        : base(actorId)
    {
        EditedBillingState = editedBillingState;
    }
}

public class BillingMarkedAsChecked : PortfolioEvent
{
    public PortfolioState.BillingState EditedBillingState { get; }

    public BillingMarkedAsChecked(ItemIdentity actorId, PortfolioState.BillingState editedBillingState)
        : base(actorId)
    {
        EditedBillingState = editedBillingState;
    }
}

public class RepetitiveBillingAdded : PortfolioEvent
{
    public PortfolioState.RepetitiveBillingState NewRepetitiveBillingState { get; }

    public RepetitiveBillingAdded(ItemIdentity actorId, PortfolioState.RepetitiveBillingState newRepetitiveBillingState)
        : base(actorId)
    {
        NewRepetitiveBillingState = newRepetitiveBillingState;
    }
}

public class RepetitiveBillingDeleted : PortfolioEvent
{
    public PortfolioState.RepetitiveBillingState RemovedRepetitiveBillingState { get; }

    public RepetitiveBillingDeleted(ItemIdentity actorId, PortfolioState.RepetitiveBillingState removedRepetitiveBillingState)
        : base(actorId)
    {
        RemovedRepetitiveBillingState = removedRepetitiveBillingState;
    }
}

public class RepetitiveBillingEdited : PortfolioEvent
{
    public PortfolioState.RepetitiveBillingState EditedRepetitiveBillingState { get; }

    public RepetitiveBillingEdited(ItemIdentity actorId, PortfolioState.RepetitiveBillingState editedRepetitiveBillingState)
        : base(actorId)
    {
        EditedRepetitiveBillingState = editedRepetitiveBillingState;
    }
}

public class PortfolioCreated : PortfolioEvent
{
    public string Name { get; }

    public PortfolioCreated(ItemIdentity actorId, string name)
        : base(actorId)
    {
        Name = name;
    }
}

public class PortfolioEdited : PortfolioEvent
{
    public string Name { get; }

    public PortfolioEdited(ItemIdentity actorId, string name)
        : base(actorId)
    {
        Name = name;
    }
}

public class PortfolioInitialized : PortfolioEvent
{
    public PortfolioState State { get; }

    public PortfolioInitialized(ItemIdentity actorId, PortfolioState state)
        : base(actorId)
    {
        State = state;
    }
}

public class NextBillingInserted : PortfolioEvent
{
    public PortfolioState.BillingState NewBillingState { get; }
    public PortfolioState.RepetitiveBillingState EditedRepetitiveBillingState { get; }

    public NextBillingInserted(ItemIdentity actorId,
        PortfolioState.BillingState newBillingState,
        PortfolioState.RepetitiveBillingState editedRepetitiveBillingState)
        : base(actorId)
    {
        NewBillingState = newBillingState;
        EditedRepetitiveBillingState = editedRepetitiveBillingState;
    }
}
