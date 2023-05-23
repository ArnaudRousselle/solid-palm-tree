using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.Converters;
using Proto.Persistence;
using System.Text.Json;

namespace MyPersonalAccounting.ActorSystem;

public interface IStore<T> : ISnapshotStore
{
}

public class Store<T> : IStore<T>
{
    private const string DIR = @"D:\tmp\accounting\";//todo: à mettre dans un fichier de configuration

    private readonly JsonSerializerOptions _options;

    public Store()
    {
        _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };
        _options.Converters.Add(new DateOnlyConverter());
        // _options.Converters.Add(new ItemIdentityConverter<PortfolioId>());
        // _options.Converters.Add(new ItemIdentityConverter<BillingId>());
        // _options.Converters.Add(new ItemIdentityConverter<RepetitiveBillingId>());
    }

    public Task DeleteSnapshotsAsync(string actorName, long inclusiveToIndex)
    {
        var path = Path.Combine(DIR, actorName + ".json");
        if (File.Exists(path))
            File.Delete(path);
        return Task.CompletedTask;
    }

    public async Task<(object? Snapshot, long Index)> GetSnapshotAsync(string actorName)
    {
        var path = Path.Combine(DIR, actorName + ".json");
        if (!File.Exists(path))
            return (null, 0);
        var json = await File.ReadAllTextAsync(path);
        var state = JsonSerializer.Deserialize<T>(json, _options);
        return (state, 0);
    }

    public async Task PersistSnapshotAsync(string actorName, long index, object snapshot)
    {
        var json = JsonSerializer.Serialize(snapshot, _options);
        var path = Path.Combine(DIR, actorName + ".json");
        await File.WriteAllTextAsync(path, json);
    }
}

