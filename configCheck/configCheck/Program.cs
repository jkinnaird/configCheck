using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using System.Net.NetworkInformation;

namespace configCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            List<NIC> interfaces = new List<NIC>();     //holds all NICs
            
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())     //loop through all interfaces
            {
                NIC temp = new NIC();                                                       //create temporary NIC object

                /*******************************************************************************************************************
                * get information from WMI class. Some properties of this class cannot be accessed from the NetworkInterfaces class
                *******************************************************************************************************************/
                System.Management.ManagementObjectSearcher status = new System.Management.ManagementObjectSearcher("SELECT NetEnabled, GUID, AdapterTypeID, Name FROM Win32_NetworkAdapter"); //get enabled, GUID
                foreach (System.Management.ManagementObject networkAdapter in status.Get())                     //cycle through each network adapter until the network adapter from ManagementObject matches the NetworkInterface, using the GUID to match them
                {
                    if (networkAdapter["NetEnabled"] != null)                                                   //if the enabled property is not null
                    {
                        if (networkAdapter["GUID"].ToString() == ni.Id)                                         //if the GUID from Win32_NetworkAdapter class equals the GUID from the NetworkInterface class
                        {
                            if (networkAdapter["NetEnabled"].ToString() == "0") { temp.IsEnabled = true; }      //get enabled property of adapter
                            else temp.IsEnabled = false;
                            temp.AdapterTypeID = Int32.Parse(networkAdapter["AdapterTypeID"].ToString());       //get type ID (ethernet, wireless, etc)
                            temp.networkCardName = networkAdapter["Name"].ToString();                           //get NIC name
                        }
                    }
                }

                /******************************************************************************************************************
                * get information from NetworkInterfaces class. Some properties of this class cannot be accessed from the WMI class
                ******************************************************************************************************************/
                IPInterfaceProperties niProps = ni.GetIPProperties();       //get IP properties of NIC
                IPv4InterfaceProperties ip4props = null;                    //Set initial null value to set proper scope of variable
                if (ni.Supports(NetworkInterfaceComponent.IPv4)) { ip4props = niProps.GetIPv4Properties();} //get IPv4 properties if interface supports IPv4
                IPAddressCollection nameServers = niProps.DnsAddresses;                                     //get DNS server list
                GatewayIPAddressInformationCollection gateways = niProps.GatewayAddresses;                  //Get Gateway information
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) { temp.isWireless = true; temp.isEthernet = false; }     //check if adapter is wireless
                else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet) { temp.isWireless = false; temp.isEthernet = true; }     //check if adapter is ethernet
                if ((ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet) && ni.Supports(NetworkInterfaceComponent.IPv4)) //if adapter is wireless or ethernet and supports IPv4 (doesn't check bluetooth, IR, or Pseudo interfaces)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            temp.GUID1 = ni.Id.ToString();                                                      //store the GUID from the NetworkInterface class
                            temp.Ip = ip.Address;
                            temp.Mask = ip.IPv4Mask;
                            temp.MacAddress = ni.GetPhysicalAddress();
                            temp.PrimaryDNS = nameServers.First();
                            temp.SecondaryDNS = nameServers.Last();
                            if (gateways.FirstOrDefault() != null) { temp.Gateway = gateways.FirstOrDefault().Address; }
                            if (ni.Supports(NetworkInterfaceComponent.IPv4)){ temp.DhcpEnabled = ip4props.IsDhcpEnabled; }

                            Console.WriteLine(temp.networkCardName);
                            Console.WriteLine(temp.Ip);
                            Console.WriteLine(temp.Mask);
                            Console.WriteLine(temp.Gateway);    //not correct info
                            Console.WriteLine(temp.MacAddress);
                            Console.WriteLine(temp.PrimaryDNS);
                            Console.WriteLine(temp.SecondaryDNS);
                            Console.WriteLine(temp.DhcpEnabled);
                            Console.WriteLine(temp.IsEnabled);
                            Console.WriteLine();

                        }
                    }
                }
                interfaces.Add(temp);
            }
            Thread.Sleep(50000);
        }
    }
}
