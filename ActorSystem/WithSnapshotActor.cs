using Proto;
using Proto.Persistence;

namespace MyPersonalAccounting.ActorSystem;

public abstract class WithSnapshotActor<TState> : IActor where TState : new()
{
    private readonly IStore<TState> _snapshotStore;
    private readonly PidInformations _pidInformations;
    private Persistence? _persistence;
    private string _name;
    protected TState State { get; set; } = new TState();

    protected WithSnapshotActor(PidInformations pidInformations, IStore<TState> snapshotStore) : this(pidInformations, snapshotStore, "")
    {
    }

    protected WithSnapshotActor(PidInformations pidInformations, IStore<TState> snapshotStore, string name)
    {
        _snapshotStore = snapshotStore;
        _pidInformations = pidInformations;
        _name = name;
    }

    private void ApplySnapshot(Snapshot snapshot)
    {
        if (snapshot.State == null)
            return;
        State = (TState)snapshot.State;
    }

    public Task<IEvent?> Event(IEvent? evt)
    {
        return Task.FromResult<IEvent?>(evt);
    }

    public Task<IEvent?> NoEvent()
    {
        return Task.FromResult<IEvent?>(null);
    }

    public async Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case Started msg:
                {
                    _persistence = Persistence.WithSnapshotting(
                        _snapshotStore,
                        !string.IsNullOrEmpty(_name) ? _name : context.Self.GetName(),
                        ApplySnapshot);
                    await _persistence.RecoverStateAsync();
                    var evt = await HandleAsync(context);
                    if (evt != null) context.Send(_pidInformations.ProjectionsRoot, evt);
                    break;
                }
            case Removed msg:
                {
                    if (_persistence != null)
                        await _persistence.DeleteSnapshotsAsync(long.MaxValue);
                    await context.StopAsync(context.Self);
                    break;
                }
            default:
                {
                    var evt = await HandleAsync(context);
                    if (_persistence != null)
                        await _persistence.PersistSnapshotAsync(State!);
                    if (evt != null) context.Send(_pidInformations.ProjectionsRoot, evt);
                    break;
                }
        }
    }

    protected abstract Task<IEvent?> HandleAsync(IContext context);
}

static class PIDExtensions
{
    public static string GetName(this PID pid)
    {
        var str = pid.ToString();
        var lastIndex = str.LastIndexOf("/");
        return str.Substring(lastIndex + 1);
    }
}

public record Removed { }
