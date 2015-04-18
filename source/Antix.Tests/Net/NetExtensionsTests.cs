using System;
using System.Net.NetworkInformation;
using Antix.Net;
using Xunit;

namespace Antix.Tests.Net
{
    public class NetExtensionsTests
    {
        [Fact]
        public void get_local_ip()
        {
            var ips = NetworkInterface
                .GetAllNetworkInterfaces()
                .GetLocalIPAddresses(NetworkInterfaceType.Ethernet, NetworkInterfaceType.Wireless80211);

            foreach (var ip in ips)
            {
                Console.WriteLine(ip);
            }
        }
    }
}