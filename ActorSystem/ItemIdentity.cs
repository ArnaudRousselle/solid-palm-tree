namespace MyPersonalAccounting.ActorSystem;

public abstract record ItemIdentity
{
    public Guid Guid { get; }

    protected ItemIdentity()
    {
        Guid = Guid.NewGuid();
    }

    protected ItemIdentity(Guid guid)
    {
        Guid = guid;
    }

    protected ItemIdentity(string guidStr)
    {
        Guid = Guid.Parse(guidStr);
    }
}
