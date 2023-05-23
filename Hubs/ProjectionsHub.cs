using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace MyPersonalAccounting.Hubs;

public abstract class ProjectionArgs
{
    [Required]
    public string ArgsType { get => this.GetType().Name; }
    [Required]
    public int Version { get; set; }
}

public interface IProjectionsClient
{
    Task ProjectionUpdated(ProjectionArgs args);
}

public class ProjectionsHub : Hub<IProjectionsClient>
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}