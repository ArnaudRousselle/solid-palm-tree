using System.Text.Json.Serialization;
using MyPersonalAccounting.Converters;

namespace MyPersonalAccounting.ActorSystem.Portfolio;

[JsonConverter(typeof(ItemIdentityConverter<BillingId>))]
public record BillingId : ItemIdentity
{
    public BillingId() : base()
    {
    }

    public BillingId(Guid guid) : base(guid)
    {
    }

    public BillingId(string guidStr) : base(guidStr)
    {
    }
}

[JsonConverter(typeof(ItemIdentityConverter<RepetitiveBillingId>))]
public record RepetitiveBillingId : ItemIdentity
{
    public RepetitiveBillingId() : base()
    {
    }

    public RepetitiveBillingId(Guid guid) : base(guid)
    {
    }

    public RepetitiveBillingId(string guidStr) : base(guidStr)
    {
    }
}
