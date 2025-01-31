namespace CretX.Services;

public interface INetworkService
{
    bool IsOnline(string ip);
    Task SendWakeOnLan(string macAddress);
    string? GetIpFromMac(string targetMac);
}