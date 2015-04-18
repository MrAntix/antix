using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Antix.Net
{
    public static class NetExtensions
    {
        public static IEnumerable<IPAddress> GetLocalIPAddresses(
            this IEnumerable<NetworkInterface> networkInterfaces, params NetworkInterfaceType[] types)
        {
            return from networkInterface in networkInterfaces.WhereUp(types)
                from unicastIPAddress in networkInterface.GetIPProperties().UnicastAddresses
                where unicastIPAddress.Address.AddressFamily == AddressFamily.InterNetwork
                select unicastIPAddress.Address;
        }

        public static IEnumerable<NetworkInterface> WhereUp(
            this IEnumerable<NetworkInterface> networkInterfaces, params NetworkInterfaceType[] types)
        {
            return networkInterfaces
                .Where(networkInterface =>
                    networkInterface.OperationalStatus == OperationalStatus.Up
                    && (!types.Any() || types.Contains(networkInterface.NetworkInterfaceType))
                );
        }
    }
}