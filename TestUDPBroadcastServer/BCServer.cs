using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestUDPBroadcastServer
{
    public partial class BCServer : Form
    {
        public BCServer()
        {
            InitializeComponent();
        }



        private void BCServer_Load(object sender, EventArgs e)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet || 
                    adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    //获取以太网卡网络接口信息
                    IPInterfaceProperties ip = adapter.GetIPProperties();
                    //获取单播地址集
                    UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipadd in ipCollection)
                    {
                        //InterNetwork    IPV4地址      InterNetworkV6        IPV6地址
                        //Max            MAX 位址
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            if (ipadd.IsDnsEligible) {
                                string desc = adapter.Description.ToLower();
                                if (!desc.Contains("vmware") && !desc.Contains("virtual")) {
                                    Console.WriteLine(desc);
                                    //判断是否为ipv4
                                    Console.WriteLine(ipadd.Address.ToString());
                                }
                            }
                        }
                    }
                }
            }           

            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 34308);  
            byte[] buf = Encoding.Default.GetBytes("Hello from UDP broadcast");  
            
            while (true)  
            {  
                client.Send(buf, buf.Length, endpoint);  
                Thread.Sleep(1000);
            }  
        }
    }
}
