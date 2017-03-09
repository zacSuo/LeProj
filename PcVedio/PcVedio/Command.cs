﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace PcVedio
{
    class Command
    {
        private const int PORT_SEARCH = 9527,PORT_VEDIO=80,BUFFER_SIZE=1024;
        private const int PORT_SELF_SEARCH = 8888, PORT_SELF_VEDIO = 8889;
        private const string ROUTE_ADDRESS = "255.255.255.255", ROUTE_ADDRESS2 = "192.168.1.255", selfAddress = "127.0.0.1";
        private string cameraIP = "192.168.1.1";
        private UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, PORT_SELF_SEARCH));
        private Socket socket = NetHelper.CreateTcpSocket();
        private bool loginFlag = false;
        private int cameraPort = 80;


        public Command()
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, PORT_SELF_VEDIO));

            Thread threadRecev = new Thread(RespConnectWifi);
            threadRecev.Start();

            Thread threadTCP = new Thread(ListenPort);
            threadTCP.Start();
        }


        public void ConnectWifi()
        {
            IPEndPoint targetPoint = new IPEndPoint(IPAddress.Broadcast, PORT_SEARCH);            
            
            byte[] buff;
            Coder.EncodeWifiSearch(out buff);
            client.EnableBroadcast = true;
            client.Send(buff, 5, targetPoint);
        }

        public void RespConnectWifi()
        {
            IPEndPoint serverPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] buff = client.Receive(ref serverPoint);
                WifiRespInfo info = Coder.DecodeWifiSearch(buff);
                Login1(info.IP,info.Port);
            }
        }

        public void Login1(string ip,int port)
        {
            if (NetHelper.Connect(socket, ip, port))
            {
                byte[] buff;
                Coder.EncodeLogin1(out buff);
                socket.Send(buff);
            }
        }

        public void ListenPort()
        {
            TcpListener listener = NetHelper.CreateTcpListener(NetHelper.LANIP, PORT_SELF_VEDIO);
            listener.Start();
            while (true)
            {
                TcpClient tcpConnector = listener.AcceptTcpClient();
                NetworkStream ns = tcpConnector.GetStream();

                byte[] buff = new byte[BUFFER_SIZE];
                int byteRead = ns.Read(buff, 0, BUFFER_SIZE);

                string strMsg = ConvertHelper.BytesToString(buff, System.Text.Encoding.UTF8);
                Console.WriteLine(strMsg);
            }

        }
    }
}
