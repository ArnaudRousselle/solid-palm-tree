using Microsoft.AspNetCore.SignalR;
using MyPersonalAccounting.Hubs;
using Proto;

namespace MyPersonalAccounting.ActorSystem;

public abstract class ProjectionActor : IActor
{
    private readonly IHubContext<ProjectionsHub, IProjectionsClient> _hubContext;
    private int _version = 0;//todo ARNAUD: trouver un moyen de reprendre la numérotation en cas de plantage

    protected ProjectionActor(IHubContext<ProjectionsHub, IProjectionsClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public abstract Task ReceiveAsync(IContext context);

    protected async Task Notify(ProjectionArgs args)
    {
        args.Version = ++_version;
        await _hubContext.Clients.All.ProjectionUpdated(args);
    }

    public void Respond<T>(IContext context, IRequest<T> request, T result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        var response = new RequestResult<T>(result, _version);
        context.Respond(response);
    }
}
