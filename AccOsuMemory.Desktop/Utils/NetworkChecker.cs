using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AccOsuMemory.Desktop.Utils;

public class NetworkChecker
{
    private const string HostName = "www.qq.com";

    public static async Task<IPStatus> CheckNetworkStatusAsync()
    {
        using Ping ping = new();
        var reply = await ping.SendPingAsync(HostName, 10000);
        return reply.Status;
    }
}