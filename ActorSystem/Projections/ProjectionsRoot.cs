using MyPersonalAccounting.ActorSystem.Projections.Billings;
using MyPersonalAccounting.ActorSystem.Projections.Portfolios;
using MyPersonalAccounting.ActorSystem.Projections.RepetitiveBillings;
using Proto;
using Proto.DependencyInjection;

namespace MyPersonalAccounting.ActorSystem.Projections;

public class ProjectionsRoot : IActor
{
    public ProjectionsRoot()
    {

    }

    public Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case Started msg:
                Handle(context, msg);
                break;
            case IEvent msg:
                foreach (var child in context.Children)
                    context.Send(child, msg);
                break;
            case IRequest msg:
                foreach (var child in context.Children)
                    context.Forward(child);
                break;
        }

        return Task.CompletedTask;
    }

    private void Handle(IContext context, Started msg)
    {
        context.Spawn(context.System.DI().PropsFor<BillingsProjection>());
        context.Spawn(context.System.DI().PropsFor<PortfoliosProjection>());
        context.Spawn(context.System.DI().PropsFor<RepetitiveBillingsProjection>());
    }
}