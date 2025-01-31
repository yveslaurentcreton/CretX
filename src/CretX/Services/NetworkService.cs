using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace CretX.Services;

public class NetworkService : INetworkService
{
    public bool IsOnline(string ip)
    {
        using var ping = new Ping();
        try
        {
            var reply = ping.Send(ip, 3000);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task SendWakeOnLan(string macAddress)
    {
        var macBytes = macAddress.Split(':').Select(x => Convert.ToByte(x, 16)).ToArray();
        var magicPacket = new byte[102];

        for (var i = 0; i < 6; i++)
            magicPacket[i] = 0xFF;

        for (var i = 1; i <= 16; i++)
            Array.Copy(macBytes, 0, magicPacket, i * 6, 6);

        using var client = new UdpClient();
        client.Connect(IPAddress.Broadcast, 9);
        await client.SendAsync(magicPacket, magicPacket.Length);
    }
    
    public string? GetIpFromMac(string targetMac)
    {
        targetMac = targetMac.Replace(":", "-").ToLower(); // Normalize MAC address
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "arp", // replace with nmap
                Arguments = "-a",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Regex pattern to extract IP-MAC pairs
        var match = Regex.Match(output, $@"(?<ip>\d+\.\d+\.\d+\.\d+)\s+.*\s+(?<mac>{targetMac})", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups["ip"].Value : null;
    }
}