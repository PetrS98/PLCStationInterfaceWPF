using Sharp7;
using System;
using System.Threading.Tasks;
using WPFUtilsLib.Net;

namespace WPFUtilsLib.PLCs.Siemens
{
    public class SiemensPLC_1 : IHasClientStatus
    {
        private object _lock = new object();

        public event EventHandler<ushort> LiveUIntChanged;
        public event EventHandler<ClientStatus> StatusChanged;

        public event EventHandler<int> ReadStatusCodeChanged;
        public event EventHandler<int> WriteStatusCodeChanged;
        public event EventHandler<int> LiveUIntStatusCodeChanged;

        public event EventHandler<byte[]> DataBufferReceived;

        private S7Client client = new S7Client();

        private System.Timers.Timer timerUpdating = new System.Timers.Timer();
        private System.Timers.Timer timerReconnecting = new System.Timers.Timer();

        private int readStatusCode = -1;
        private int writeStatusCode = -1;
        private int liveUIntStatusCode = -1;

        private ushort liveUInt = 0;
        private bool reconnectEnabled = false;
        private byte[] readDataBuffer = null;
        private byte[] writeDataBuffer = null;
        private ClientStatus status;

        public int ReadStatusCode
        {
            get => readStatusCode;
            private set
            {
                bool changed = readStatusCode != value;
                readStatusCode = value;
                /*if (changed)*/ ReadStatusCodeChanged?.Invoke(this, value);
            }
        }

        public int WriteStatusCode
        {
            get => writeStatusCode;
            private set
            {
                bool changed = writeStatusCode != value;
                writeStatusCode = value;
                /*if (changed)*/ WriteStatusCodeChanged?.Invoke(this, value);
            }
        }

        public int LiveUIntStatusCode
        {
            get => liveUIntStatusCode;
            private set
            {
                bool changed = liveUIntStatusCode != value;
                liveUIntStatusCode = value;
                /*if (changed)*/ LiveUIntStatusCodeChanged?.Invoke(this, value);
            }
        }


        public ushort LiveUInt
        {
            get => liveUInt;
            private set
            {
                Status = liveUInt != value ? ClientStatus.Connected : ClientStatus.Disconnected;
                liveUInt = value;
                if (Status == ClientStatus.Connected) LiveUIntChanged?.Invoke(this, value);
            }
        }

        public ClientStatus Status
        {
            get => status;
            private set
            {
                bool changed = status != value;
                status = value;
                StatusChanged?.Invoke(this, value);
                if (!changed) return;
                if (value == ClientStatus.Connected) timerUpdating.Start();
                else timerUpdating.Stop();
                UpdateReconnectingTimer();
                //StatusChanged?.Invoke(this, value);
            }
        }

        public int UpdateInterval { get => (int)timerUpdating.Interval; set => timerUpdating.Interval = value; }
        public int ReconnectInterval { get => (int)timerReconnecting.Interval; set => timerReconnecting.Interval = value; }
        public bool ReconnectEnabled { get => reconnectEnabled; set { reconnectEnabled = value; UpdateReconnectingTimer(); } }
        public byte[] ReadDataBuffer { get => readDataBuffer; private set { readDataBuffer = value; DataBufferReceived?.Invoke(this, value); } }
        public byte[] WriteDataBuffer { get { return writeDataBuffer; } set { writeDataBuffer = value; } }

        public string IPAddress { get; set; } = "192.168.1.25";
        public int Rack { get; set; } = 0;
        public int Slot { get; set; } = 1;

        public int ReadDBNumber { get; set; } = 1;
        public int ReadDataBufferOffset { get; set; } = 0;
        public int ReadDataBufferSize { get; set; } = 1;

        public int WriteDBNumber { get; set; } = 1;
        public int WriteDataBufferOffset { get; set; } = 0;
        public int WriteDataBufferSize { get; set; } = 1;

        public int LiveUIntDBNumber { get; set; } = 0;
        public int LiveUIntOffset { get; set; } = 0;
        public int LiveUIntBufferSize { get; set; } = 1;

        public SiemensPLC_1()
        {
            timerUpdating.Interval = 150;
            timerReconnecting.Interval = 5000;
            timerUpdating.Elapsed += UpdateData;
            timerReconnecting.Elapsed += TryReconnect;

            Status = ClientStatus.Disconnected;
        }



        public void Connect()
        {
            Status = ClientStatus.Connecting;
            LiveUIntStatusCode = client.ConnectTo(IPAddress, Rack, Slot);
            Status = LiveUIntStatusCode == 0 ? ClientStatus.Connected : ClientStatus.Disconnected;
        }

        public async Task ConnectAsync() => await Task.Run(Connect);
        private void TryReconnect(object sender, EventArgs e) => Connect();

        public void Disconnect()
        {
            client.Disconnect();
            Status = ClientStatus.Disconnected;
        }

        private void UpdateReconnectingTimer()
        {
            if (reconnectEnabled && status == ClientStatus.Disconnected) timerReconnecting.Start();
            else timerReconnecting.Stop();
        }

        private void UpdateData(object sender, EventArgs e)
        {
            ReadLiveUIntFromPLC();
            ReadDataFromPLC();
            WriteDataToPLC();
        }

        private void ReadLiveUIntFromPLC()
        {
            lock (_lock)
            {
                byte[] buffer = new byte[LiveUIntBufferSize];

                LiveUIntStatusCode = client.DBRead(LiveUIntDBNumber, LiveUIntOffset, LiveUIntBufferSize, buffer);
                Status = LiveUIntStatusCode == 0 ? ClientStatus.Connected : ClientStatus.Disconnected;
                if (Status == ClientStatus.Disconnected) return;

                LiveUInt = S7.GetUIntAt(buffer, 0);
            }
        }

        private void ReadDataFromPLC()
        {
            lock (_lock)
            {
                byte[] buffer = new byte[ReadDataBufferSize];

                ReadStatusCode = client.DBRead(ReadDBNumber, ReadDataBufferOffset, ReadDataBufferSize, buffer);
                ReadDataBuffer = buffer;
            }
        }

        private void WriteDataToPLC()
        {
            lock (_lock)
            {
                byte[] buffer = new byte[WriteDataBufferSize];

                WriteStatusCode = client.DBWrite(WriteDBNumber, WriteDataBufferOffset, WriteDataBufferSize, buffer);
            }
        }

        public string GetErrorMessage(int ErrorCode)
        {
            return client.ErrorText(ErrorCode);
        }
    }
}
