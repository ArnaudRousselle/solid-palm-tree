using System.Reflection;
using Microsoft.OpenApi.Models;
using MyPersonalAccounting.ActorSystem;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.ActorSystem.Projections;
using MyPersonalAccounting.ActorSystem.Projections.Billings;
using MyPersonalAccounting.ActorSystem.Projections.Portfolios;
using MyPersonalAccounting.ActorSystem.Projections.RepetitiveBillings;
using MyPersonalAccounting.Converters;
using MyPersonalAccounting.Hubs;
using MyPersonalAccounting.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(configure =>
    {
        configure.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        // configure.JsonSerializerOptions.Converters.Add(new ItemIdentityConverter<PortfolioId>());
        // configure.JsonSerializerOptions.Converters.Add(new ItemIdentityConverter<BillingId>());
        // configure.JsonSerializerOptions.Converters.Add(new ItemIdentityConverter<RepetitiveBillingId>());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<CustomDocumentFilter>();
    c.SchemaFilter<CustomSchemaFilter>();

    Assembly
        .GetAssembly(typeof(Program))?
        .GetTypes()
        .Where(t => typeof(ItemIdentity).IsAssignableFrom(t) && t != typeof(ItemIdentity))
        .ToList()
        .ForEach(t =>
        {
            c.MapType(t, () => new OpenApiSchema
            {
                Type = "string",
                Format = "uuid"
            });
        });
});
builder.Services.AddSignalR();

builder.Services.AddSpaStaticFiles(config => config.RootPath = "ClientApp");

builder.Services.AddTransient(typeof(IStore<>), typeof(Store<>));

builder.Services.AddTransient<PortfoliosProjection>();
builder.Services.AddTransient<BillingsProjection>();
builder.Services.AddTransient<RepetitiveBillingsProjection>();
builder.Services.AddTransient<ProjectionsRoot>();

builder.Services.AddTransient<PortfolioManager>();
builder.Services.AddTransient<Portfolio>();

builder.Services.AddSingleton<PidInformations>();

builder.Services.AddSingleton<ICommandHub>(serviceProvider =>
{
    var actorSystem = new AccountingActorSystem(serviceProvider);
    return actorSystem;
});

var app = builder.Build();

var actorSystem = (AccountingActorSystem)app.Services.GetRequiredService<ICommandHub>();
await actorSystem.Initialize();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller}/{action=Index}/{id?}");

    endpoints.MapHub<ProjectionsHub>("/projectionsHub");
});

app.UseSpaStaticFiles();
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

app.Run();
