using CretX.Configuration;
using CretX.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CretX.Controllers;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    private readonly CretXConfig _cretxConfig;
    private readonly INetworkService _networkService;

    public StatusController(IOptions<CretXConfig> cretxConfig, INetworkService networkService)
    {
        _cretxConfig = cretxConfig.Value;
        _networkService = networkService;
    }
    
    [HttpGet]
    public IActionResult GetStatus()
    {
        var macAddress = _cretxConfig.MacAddress;
        var ipAddress = _networkService.GetIpFromMac(macAddress);
        
        if (ipAddress == null)
            return NotFound(new { status = "offline" });
        
        var isOnline = _networkService.IsOnline(ipAddress);

        if (!isOnline)
            return NotFound(new { status = "offline" });

        return Ok(new { status = "online" });
    }
}