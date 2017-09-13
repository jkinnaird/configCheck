using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace configCheck
{
    class NIC
    {
        public bool isWireless;
        public bool isEthernet;

        private bool dhcpEnabled;
        private PhysicalAddress macAddress;
        private IPAddress gateway;
        private bool isEnabled;
        private bool IPV6enabled;
        private IPAddress primaryDNS;
        private IPAddress secondaryDNS;
        private IPAddress ip;
        private IPAddress mask;
        private string NICname;
        private int adapterTypeID;
        private string GUID;

        public bool DhcpEnabled
        {
            get
            {
                return dhcpEnabled;
            }

            set
            {
                dhcpEnabled = value;
            }
        }

        public PhysicalAddress MacAddress
        {
            get
            {
                return macAddress;
            }

            set
            {
                macAddress = value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                isEnabled = value;
            }
        }

        public bool IPV6enabled1
        {
            get
            {
                return IPV6enabled;
            }

            set
            {
                IPV6enabled = value;
            }
        }

        public IPAddress PrimaryDNS
        {
            get
            {
                return primaryDNS;
            }

            set
            {
                primaryDNS = value;
            }
        }

        public IPAddress SecondaryDNS
        {
            get
            {
                return secondaryDNS;
            }

            set
            {
                secondaryDNS = value;
            }
        }

        public IPAddress Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public string networkCardName
        {
            get
            {
                return NICname;
            }

            set
            {
                NICname = value;
            }
        }

        public IPAddress Mask
        {
            get
            {
                return mask;
            }

            set
            {
                mask = value;
            }
        }

        public IPAddress Gateway
        {
            get
            {
                return gateway;
            }

            set
            {
                gateway = value;
            }
        }

        public int AdapterTypeID
        {
            get
            {
                return adapterTypeID;
            }

            set
            {
                adapterTypeID = value;
            }
        }

        public string GUID1
        {
            get
            {
                return GUID;
            }

            set
            {
                GUID = value;
            }
        }

        public NIC()
        {
            //set all variables
        }


    }
}
