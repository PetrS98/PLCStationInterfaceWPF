using System;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;
using WPFUtilsLib.Net;

namespace WPFUtilsLib.TCPIP
{
    public class Client : IHasClientStatus
    {
        SimpleTcpClient _client;

        readonly private bool RECONNECT_ENABLE = true;
        private bool Reconnecting = false;

        System.Timers.Timer ReconnectingTimer = new System.Timers.Timer();
        System.Timers.Timer CheckConnection = new System.Timers.Timer();

        public event EventHandler<ClientStatus> StatusChanged;
        public event EventHandler<string> RecieveDataChanged;
        public event EventHandler<Exception> ExceptionChanged;

        private ClientStatus status = ClientStatus.Disconnected;
        public ClientStatus Status
        {
            get { return status; }
            private set
            {
                bool changed = status != value;
                status = value;
                if (changed) StatusChanged?.Invoke(this, value);
            }
        }

        private string recieveData;

        public string RecieveData
        {
            get { return recieveData; }
            set 
            { 
                bool changed = recieveData != value;
                recieveData = value;
                if (changed) RecieveDataChanged?.Invoke(this, value);
            }
        }

        private string sendData;

        public string SendData
        {
            get { return sendData; }
            set 
            { 
                sendData = value;
                SendDataCMD(value);
            }
        }


        public string IPAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8080;

        public Client()
        {
            ReconnectingTimer.Interval = 5000;
            ReconnectingTimer.Elapsed += TryReconnect;

            CheckConnection.Interval = 300;
            CheckConnection.Elapsed += CheckComm;


            if (RECONNECT_ENABLE == true)
            {
                ReconnectingTimer.Start();
            }
        }

        private void CheckComm(object sender, EventArgs e)
        {
            if (_client.IsConnected) Status = ClientStatus.Connected;
            else Status = ClientStatus.Disconnected;
        }

        private void TryReconnect(object sender, EventArgs e)
        {
            if (_client.IsConnected == true) return;

            Status = ClientStatus.Connecting;
            Reconnecting = true;
            Disconnect(false);
            Connect_Async();
        }

        async public void Connect_Async()
        {
            await Task.Run(() => Connect());
        }

        public bool Connect()
        {
            if (RECONNECT_ENABLE == true)
            {
                ReconnectingTimer.Start();
            }

            try
            {
                if (!(_client is null))
                {
                    _client.Events.DataReceived -= Client_DataReceived;
                }

                _client = new SimpleTcpClient(IPAddress, Port);
                _client.Events.DataReceived += Client_DataReceived;
                _client.Connect();
                Status = ClientStatus.Connected;
                return true;
            }
            catch(Exception ex)
            {
                if (Reconnecting == false)
                    ExceptionChanged(this, ex);
                return false;
            } 
        }

        public void Disconnect(bool DisableReconnect)
        {
            _client.Disconnect();
            Status = ClientStatus.Disconnected;

            if (DisableReconnect) ReconnectingTimer.Stop();
        }

        private void Client_DataReceived(object sender, DataReceivedEventArgs e)
        {
            RecieveData = Encoding.UTF8.GetString(e.Data);
        }

        private void SendDataCMD(string Data)
        {
            if (Status == ClientStatus.Connected)
            {
                try
                {
                    _client.Send(Data);
                }
                catch (Exception ex)
                {
                    ExceptionChanged(this, ex);
                }
            }
        }
    }
}
