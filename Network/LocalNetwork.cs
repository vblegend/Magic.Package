using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Network
{
    public class LocalNetwork
    {

        public static IEnumerable<IPAddress> GetAllIpAddress()
        {
            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Adapter in fNetworkInterfaces)
            {
                if (Adapter.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation UnicastIPAddressInformation in Adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            yield return UnicastIPAddressInformation.Address; 
                        }
                    }
                }
            }
        }

        public static IPAddress GetActiveIpAddress()
        {
            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Adapter in fNetworkInterfaces)
            {
                if (Adapter.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation UnicastIPAddressInformation in Adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return UnicastIPAddressInformation.Address;
                        }
                    }
                }
            }
            return null;
        }






    }
}
