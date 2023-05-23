using System.Collections.Immutable;
using Proto;
using Proto.DependencyInjection;

namespace MyPersonalAccounting.ActorSystem.Portfolio;

public class PortfolioManager : WithSnapshotActor<PortfolioManagerState>
{
    private readonly Dictionary<PortfolioId, PID> _mappings = new Dictionary<PortfolioId, PID>();

    public PortfolioManager(PidInformations pidInformations, IStore<PortfolioManagerState> store) : base(pidInformations, store, "PortfolioManager")
    {
    }

    protected override Task<IEvent?> HandleAsync(IContext context)
    {
        switch (context.Message)
        {
            case CreatePortfolio msg:
                Handle(context, msg);
                break;
            case DeletePortfolio msg:
                Handle(context, msg);
                break;
            case PortfolioMessage msg:
                Handle(context, msg);
                break;
            case Started msg:
                Handle(context, msg);
                break;
        }

        return Task.FromResult<IEvent?>(null);
    }

    private void Handle(IContext context, CreatePortfolio msg)
    {
        var portfolioId = new PortfolioId();
        State = State with { Portfolios = State.Portfolios.Add(portfolioId) };
        var pid = CreateChild(context, portfolioId);
        context.Send(pid, msg);
    }

    private void Handle(IContext context, DeletePortfolio msg)
    {
        State = State with { Portfolios = State.Portfolios.Remove(msg.PortfolioId) };
        if (!_mappings.TryGetValue(msg.PortfolioId, out PID? pid))
            return;
        context.Send(pid, new Removed());
    }

    private void Handle(IContext context, PortfolioMessage msg)
    {
        if (!_mappings.TryGetValue(msg.PortfolioId, out PID? pid))
            return;
        context.Send(pid, msg);
    }

    private void Handle(IContext context, Started msg)
    {
        foreach (var id in State.Portfolios)
            CreateChild(context, id);
    }

    private PID CreateChild(IContext context, PortfolioId id)
    {
        var props = context.System.DI().PropsFor<Portfolio>();
        var pid = context.SpawnNamed(props, "portfolio-" + id.Guid.ToString());
        _mappings[id] = pid;
        return pid;
    }
}

public record PortfolioManagerState
{
    public ImmutableList<PortfolioId> Portfolios { get; init; } = ImmutableList.Create<PortfolioId>();
}