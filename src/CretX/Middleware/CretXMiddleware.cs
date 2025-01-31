using CretX.Configuration;
using CretX.Services;
using Microsoft.Extensions.Options;

namespace CretX.Middleware;

public class CretXMiddleware : IMiddleware
{
    private readonly CretXConfig _cretxConfig;
    private readonly INetworkService _networkService;

    public CretXMiddleware(IOptions<CretXConfig> cretxConfig, INetworkService networkService)
    {
        _cretxConfig = cretxConfig.Value;
        _networkService = networkService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var macAddress = _cretxConfig.MacAddress;
        var targetIp = _networkService.GetIpFromMac(macAddress);
        
        if (context.Request.Path == "/" && (targetIp == null || !_networkService.IsOnline(targetIp)))
        {
            Console.WriteLine("Machine is offline. Sending Wake-on-LAN...");
            await _networkService.SendWakeOnLan(macAddress);
            
            var redirectUrl = $"{context.Request.Scheme}://{context.Request.Host}/status";
            Console.WriteLine($"🔀 Redirecting to: {redirectUrl}");
            
            context.Response.Redirect(redirectUrl, permanent: false);
            return;
        }

        await next(context);
    }
}