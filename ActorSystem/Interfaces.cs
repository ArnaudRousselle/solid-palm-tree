using System.Diagnostics.CodeAnalysis;
using Proto;

namespace MyPersonalAccounting.ActorSystem;

public interface IEvent
{
    ItemIdentity ActorId { get; }
}

public interface IRequest
{
}

public interface IRequest<T> : IRequest
{
}
