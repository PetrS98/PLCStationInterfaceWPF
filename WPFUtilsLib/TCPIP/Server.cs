﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;
using WPFUtilsLib.MessageBoxes;
using WPFUtilsLib.Net;

namespace WPFUtilsLib.TCPIP
{
    public class Server : IHasServerStatus
    {
        SimpleTcpServer _server;

        public event EventHandler<ServerStatus> StatusChanged;
        public event EventHandler<string> DataReceived;

        protected System.Timers.Timer _timerUpdating { get; } = new System.Timers.Timer();

        public ushort Port { get; set; } = 8080;
        public string IPAddress { get; set; } = "127.0.0.1";
        public string Data { get; private set; }

        private ServerStatus status = ServerStatus.Stopped;
        public ServerStatus Status
        {
            get => status;
            private set
            {
                bool changed = status != value;
                status = value;
                if (changed)
                {
                    StatusChanged?.Invoke(this, value);
                }
            }
        }

        public int BroadcastInterval { get => (int)_timerUpdating.Interval; set => _timerUpdating.Interval = value; }

        public Server()
        {
            StatusChanged += StartStopTimer;
        }

        public void Start()
        {
            bool validIP = System.Net.IPAddress.TryParse(IPAddress, out System.Net.IPAddress _IPAddress);
            if (!validIP) return;
            Status = ServerStatus.Starting;

            if (_server != null)
            {
                _server.Events.DataReceived -= serverDataReceived;
            }

            _server = new SimpleTcpServer(_IPAddress + ":" + Port.ToString());
            _server.Start();
            _server.Events.DataReceived += serverDataReceived;
            Status = _server.IsListening ? ServerStatus.Running : ServerStatus.Stopped;
        }

        public async Task StartAsync()
        {
            await Task.Run(Start);
        }

        public void Stop()
        {
            _server.Stop();
            Status = _server.IsListening ? ServerStatus.Running : ServerStatus.Stopped;
        }
        private void serverDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = Encoding.UTF8.GetString(e.Data).ToString();
            data = data.Replace("\u0013", "");

            if (data == "COMM_CHECK")
                _server.Send(e.IpPort, "OK");
            else
            {
                Data = data;
                DataReceived?.Invoke(this, Data);
            }
        }

        public void Broadcast(string data)
        {
            List<string> ServerClients = (List<string>)_server.GetClients();

            foreach (string Client in ServerClients)
            {
                _server.Send(Client, data);
            }
        }

        public async Task BroadcastAsync(string data)
        {
            await Task.Run(() => Broadcast(data));
        }

        private void StartStopTimer(object sender, ServerStatus e)
        {
            if (status == ServerStatus.Running) _timerUpdating.Start();
            else if (status == ServerStatus.Stopped) _timerUpdating.Stop();
        }
    }
}