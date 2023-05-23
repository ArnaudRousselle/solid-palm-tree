using System.ComponentModel.DataAnnotations;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.ActorSystem.Projections;
using Proto;
using Proto.DependencyInjection;

namespace MyPersonalAccounting.ActorSystem;

public class RequestResult<T>
{
    [Required]
    public T Result { get; set; }
    [Required]
    public int Version { get; set; }

    public RequestResult(T result, int version)
    {
        Result = result;
        Version = version;
    }
}

public interface ICommandHub
{
    void Send(object msg);
    Task<RequestResult<T>> RequestAsync<T>(IRequest<T> query, CancellationToken cancellationToken = default);
}

public class PidInformations
{
    private PID? _portfolioManager;
    private PID? _projectionsRoot;

    public PID PortfolioManager
    {
        get => _portfolioManager ?? throw new Exception("Not initialized");
        set
        {
            if (_portfolioManager != null)
                throw new Exception("Already initialized");
            _portfolioManager = value;
        }
    }
    public PID ProjectionsRoot
    {
        get => _projectionsRoot ?? throw new Exception("Not initialized");
        set
        {
            if (_projectionsRoot != null)
                throw new Exception("Already initialized");
            _projectionsRoot = value;
        }
    }
}

public class AccountingActorSystem : ICommandHub
{
    private readonly Proto.ActorSystem _actorSystem;
    private readonly PidInformations _pidInformations;

    public AccountingActorSystem(IServiceProvider serviceProvider)
    {
        _actorSystem = new Proto.ActorSystem().WithServiceProvider(serviceProvider);
        _pidInformations = serviceProvider.GetRequiredService<PidInformations>();
    }

    public async Task Initialize()
    {
        _pidInformations.ProjectionsRoot = _actorSystem.Root.Spawn(_actorSystem.DI().PropsFor<ProjectionsRoot>());
        await Task.Delay(2500);
        _pidInformations.PortfolioManager = _actorSystem.Root.Spawn(_actorSystem.DI().PropsFor<PortfolioManager>());
    }

    public void Send(object msg)
    {
        _actorSystem.Root.Send(_pidInformations.PortfolioManager, msg);
    }

    public async Task<RequestResult<T>> RequestAsync<T>(IRequest<T> query, CancellationToken cancellationToken = default)
    {
        return await _actorSystem.Root.RequestAsync<RequestResult<T>>(_pidInformations.ProjectionsRoot, query, cancellationToken);
    }

}