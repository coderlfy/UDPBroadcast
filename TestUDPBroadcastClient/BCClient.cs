using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestUDPBroadcastClient
{
    public partial class BCClient : Form
    {
        public BCClient()
        {
            InitializeComponent();
        }

        private void BCClient_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(RecvThread));
            t.IsBackground = true;
            t.Start();

            string t1 = "women";
            string t2 = "women1";

            Console.WriteLine(CRC32.GetCRC32(Encoding.Default.GetBytes(t1)));
            Console.WriteLine(CRC32.GetCRC32(Encoding.Default.GetBytes(t2)));
        }
        static void RecvThread()  
        {  
            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 34308));  
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);  
            while (true)  
            {  
                byte[] buf = client.Receive(ref endpoint);  
                string msg = Encoding.Default.GetString(buf);  
                Console.WriteLine(msg);  
            }  
        }  
    }
}
