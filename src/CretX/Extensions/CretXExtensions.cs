using CretX.Configuration;
using CretX.Middleware;
using DotNetJet.FluentValidation.DependencyInjection.Extensions;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace CretX.Extensions;

public static class CretXExtensions
{
    public static void AddCretX(this WebApplicationBuilder builder)
    {
        // Configuration
        var configuration = builder.Configuration;
        
        var cretxConfigSection = configuration.GetSection(CretXConfig.Section);
        builder.Services.AddOptions<CretXConfig>()
            .Bind(cretxConfigSection)
            .ValidateFluentValidation()
            .ValidateOnStart();
        var cretxConfig = cretxConfigSection.Get<CretXConfig>();

        // Yarp
        builder.Services.AddReverseProxy()
            .LoadFromMemory([
                    new RouteConfig()
                    {
                        RouteId = "default",
                        ClusterId = "target-cluster",
                        Match = new RouteMatch { Path = "{**catchall}" },
                    }
                ],
                [
                    new ClusterConfig()
                    {
                        ClusterId = "target-cluster",
                        Destinations = new Dictionary<string, DestinationConfig>
                        {
                            { "target", new DestinationConfig { Address = cretxConfig?.TargetBaseUrl ?? string.Empty } }
                        }
                    }
                ])
            .AddTransforms(context =>
            {
                context.AddRequestHeader("Cache-Control", "no-store");
                context.AddResponseHeader("Cache-Control", "no-store");
            });
        
        // Middleware
        builder.Services.AddScoped<CretXMiddleware>();
    }
    
    public static void UseCretX(this WebApplication app)
    {
        app.UseMiddleware<CretXMiddleware>();
    }
}