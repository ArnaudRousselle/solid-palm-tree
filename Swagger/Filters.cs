using Microsoft.OpenApi.Models;
using MyPersonalAccounting.Hubs;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyPersonalAccounting.Swagger;

public class CustomDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.SchemaGenerator.GenerateSchema(typeof(ProjectionArgs), context.SchemaRepository);
    }
}

public class CustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        if (type == typeof(ProjectionArgs))
        {
            var events = GetEvents();

            schema.Discriminator = new OpenApiDiscriminator();
            schema.Discriminator.PropertyName = "ArgsType";
            schema.Discriminator.Mapping = events.ToDictionary(a => a.Name, a => a.Name);
            schema.OneOf = events.Select(e => context.SchemaGenerator.GenerateSchema(e, context.SchemaRepository)).ToList();
        }
    }

    private static List<Type> GetEvents()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(i => !i.IsDynamic)
            .ToList();

        var types = assemblies
            .SelectMany(a => a.ExportedTypes)
            .Where(t => typeof(ProjectionArgs).IsAssignableFrom(t) && t.IsClass && t != typeof(ProjectionArgs))
            .ToList();

        return types;
    }
}