using System;
using System.Data;
using System.Threading.Tasks;
using WPFUtilsLib.Net;
using MySqlConnector;
using PLCStationInterfaceWPF.UDT;

namespace PLCStationInterfaceWPF.Classes
{
    public class MySQLDatabase : IHasClientStatus
    {
        private readonly bool RECONNECT_ENABLE = true;

        private MySqlConnection mySqlConnection;

        private System.Timers.Timer _timerStatus = new System.Timers.Timer();
        private System.Timers.Timer ReconnectingTimer = new System.Timers.Timer();

        private Exception ExceptionMemory = new Exception();

        public string DatabaseName { get; set; }
        public string IPAddress { get; set; }
        public string NonOpMessageTableName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private ClientStatus status;
        public ClientStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                StatusChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<ClientStatus> StatusChanged;
        public event EventHandler<Exception> ExceptionChanged;

        public MySQLDatabase()
        {
            _timerStatus.Interval = 150;
            _timerStatus.Elapsed += CheckStatus;
            _timerStatus.Start();

            ReconnectingTimer.Interval = 5000;
            ReconnectingTimer.Elapsed += TryReconnect;
            //ReconnectingTimer.Start();

            Status = ClientStatus.Disconnected;
        }

         private void TryReconnect(object sender, EventArgs e)
        {
            if (Status == ClientStatus.Connected || Status == ClientStatus.Connecting) return;

            Status = ClientStatus.Connecting;
            DisconnectFromDB(false);
            ConnectToDB_Async();
        }

        async public void ConnectToDB_Async()
        {
            await Task.Run(() => ConnectToDB());
        }

        public bool ConnectToDB()
        {
            if (RECONNECT_ENABLE == true)
            {
                ReconnectingTimer.Start();
            }

            try
            {
                mySqlConnection = new MySqlConnection("server=" + IPAddress + ";" + "uid=" + UserName + ";" + "pwd=" + Password + ";" + "database=" + DatabaseName);
                mySqlConnection.Open();
                Status = ClientStatus.Connected;
                return true;
            }
            catch(Exception ex)
            {
                if (ex.Message != ExceptionMemory.Message) ExceptionChanged(this, ex);
                ExceptionMemory = ex;
                
                return false;
            }
        }

        public void DisconnectFromDB(bool DisableReconnect)
        {
            Status = ClientStatus.Disconnected;
            mySqlConnection?.Close();

            if(DisableReconnect) ReconnectingTimer.Stop();
        }

        public NonOperationMessagesUDT ReadNonOperation(int NonOperationID)
        {
            using MySqlCommand mySqlCommand = new MySqlCommand();
            NonOperationMessagesUDT NonOpMess = new NonOperationMessagesUDT();

            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandText = @"SELECT * FROM " + NonOpMessageTableName + " WHERE id_non_operation = @NonOpID;";

            mySqlCommand.Parameters.AddWithValue("@NonOpID", NonOperationID);

            try
            {
                using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                mySqlDataReader.Read();

                NonOpMess.ENGMessage = mySqlDataReader.GetString(2);
                NonOpMess.CZMessage = mySqlDataReader.GetString(3);
            }
            catch (Exception ex)
            {
                ExceptionChanged(this, ex);
                NonOpMess = null;
            }

            return NonOpMess;
        }

        private void CheckStatus(object sender, EventArgs e)
        {
            var state = mySqlConnection.State;

            if (state == ConnectionState.Open)
            {
                Status = ClientStatus.Connected;
            }
            else if (state == ConnectionState.Connecting)
            {
                Status = ClientStatus.Connecting;
            }
            else
            {
                Status = ClientStatus.Disconnected;
            }
        }
    }
}
