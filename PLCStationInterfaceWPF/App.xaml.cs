using PLCStationInterfaceWPF.UDT;
using PLCStationInterfaceWPF.Windows;
using PLCStationInterfaceWPF.Windows.Settings;
using PLCStationInterfaceWPF.Windows.Testing;
using WPFUtilsLib.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PLCStationInterfaceWPF.Classes;
using WPFUtilsLib.TCPIP;
using WPFUtilsLib.PLCs.Siemens;
using PLCStationInterfaceWPF.JDO;

namespace PLCStationInterfaceWPF
{
    public partial class App : Application
    {
        private readonly string SETTING_FILE_PATH = "settings.json";
        private readonly string ENCRIPTION_KEY = "W]rs6^%]";

        public SettingsJDO Settings { get; set; } = new SettingsJDO();

        private MySQLDatabase _mySQLDatabase;
        private Server _server;
        private SiemensPLC_1 _plc;

        private MainMenu _mainMenu;

        private PLCSettings _plcSettings;
        private StationTCPServerSettings _stationTCPServerSettings;
        private AboutApp _aboutApp;
        private Diagnostics _diagnostics;
        private InterfaceData _interfaceData;
        private DatabaseSettings _databaseSettings;

        public App()
        {
            ReadSettingsJSON(SETTING_FILE_PATH, ENCRIPTION_KEY);

            TestingMethod();

            CreateDB_SetParametrAndTryConnect(ref _mySQLDatabase, Settings.DatabaseSettings);
            CreateServer_SetParametrAndTryConnect(ref _server, Settings.TCPServerSettings);
            CreatePLC_SetParameterAndTryConnect(ref _plc, Settings.PLCSettings);

            _plcSettings = new PLCSettings(Settings.PLCSettings, _plc);
            _stationTCPServerSettings = new StationTCPServerSettings(Settings.TCPServerSettings, _server);
            _diagnostics = new Diagnostics(_plc, _server, _mySQLDatabase);
            _interfaceData = new InterfaceData();
            _databaseSettings = new DatabaseSettings(Settings.DatabaseSettings, _mySQLDatabase);
            _aboutApp = new AboutApp();

            _mainMenu = new MainMenu(_plcSettings, _stationTCPServerSettings, _aboutApp, _diagnostics, _interfaceData, _databaseSettings);
            _mainMenu.Show();

            Test tst = new Test();
            tst.Show();
        }

        private void ReadSettingsJSON(string Path, string CryptKey)
        {
            if (File.Exists(Path))
            {
                FileEncriptionManager.DecryptFile(Path, CryptKey);
                Settings = SettingsJDO.Deserialize(File.ReadAllText(Path));
            }
            else
            {
                File.WriteAllText(Path, Settings.Serialize());
            }

            FileEncriptionManager.EncryptFile(Path, CryptKey);
        }
        private void WriteSettingsJSON(string Path, string CryptKey)
        {
            FileEncriptionManager.DecryptFile(Path, CryptKey);
            File.WriteAllText(Path, Settings.Serialize());
            FileEncriptionManager.EncryptFile(Path, CryptKey);
        }
        private void CreatePLC_SetParameterAndTryConnect(ref SiemensPLC_1 plc, PLCSettingsJDO settings)
        {
            plc = new SiemensPLC_1();

            plc.IPAddress = settings.IPAddress;
            plc.Rack = settings.Rack;
            plc.Slot = settings.Slot;
            plc.ReconnectEnabled = false;

            plc.LiveUIntDBNumber = settings.LiveUIntDBNumber;
            plc.LiveUIntOffset = settings.LiveUIntOffset;
            plc.LiveUIntBufferSize = settings.LiveUIntBufferSize;

            plc.ReadDBNumber = settings.ReadDBNumber;
            plc.ReadDataBufferOffset = settings.ReadDataBufferOffset;
            plc.ReadDataBufferSize = settings.ReadDataBufferSize;

            plc.WriteDBNumber = settings.WriteDBNumber;
            plc.WriteDataBufferOffset = settings.WriteDataBufferOffset;
            plc.WriteDataBufferSize = settings.WriteDataBufferSize;

#pragma warning disable CS4014
            //plc.ConnectAsync();
#pragma warning restore CS4014
        }
        private void CreateDB_SetParametrAndTryConnect(ref MySQLDatabase db, DatabaseSettingsJDO settings)
        {
            db = new MySQLDatabase();

            db.IPAddress = settings.IPAddress;
            db.DatabaseName = settings.DatabaseName;
            db.NonOpMessageTableName = settings.NonOPMessageTable;
            db.UserName = settings.DatabaseUserName;
            db.Password = settings.DatabasePassword;

            db.ConnectToDB_Async();
        }
        private void CreateServer_SetParametrAndTryConnect(ref Server server, TCPServerSettingsJDO settings)
        {
            server = new Server();

            server.IPAddress = settings.IPAddress;
            server.Port = (ushort)settings.Port;

#pragma warning disable CS4014
            server.StartAsync();
#pragma warning restore CS4014
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            WriteSettingsJSON(SETTING_FILE_PATH, ENCRIPTION_KEY);
        }

        private void TestingMethod()
        {
            //Settings.PLCSettings.IPAddress = "192.168.0.1";
            //Settings.PLCSettings.Rack = 0;
            //Settings.PLCSettings.Slot = 1;
            //Settings.PLCSettings.UpdateInterval = 100;

            //Settings.PLCSettings.LiveUIntDBNumber = 10;
            //Settings.PLCSettings.LiveUIntOffset = 0;
            //Settings.PLCSettings.LiveUIntBufferSize = 2;

            //Settings.PLCSettings.ReadDBNumber = 10;
            //Settings.PLCSettings.ReadDataBufferOffset = 34;
            //Settings.PLCSettings.ReadDataBufferSize = 8;

            //Settings.PLCSettings.WriteDBNumber = 10;
            //Settings.PLCSettings.WriteDataBufferOffset = 24;
            //Settings.PLCSettings.WriteDataBufferSize = 10;

            //-------------------------------------------------------

            Settings.DatabaseSettings.IPAddress = "213.129.135.117";
            Settings.DatabaseSettings.DatabaseName = "db_visual_inspection";
            Settings.DatabaseSettings.NonOPMessageTable = "non_operation";
            Settings.DatabaseSettings.DatabaseUserName = "StationClient";
            Settings.DatabaseSettings.DatabasePassword = "123698547";

            //------------------------------------------------------------------

            Settings.TCPServerSettings.IPAddress = "192.168.1.5";
            Settings.TCPServerSettings.Port = 8080;
        }
    }
}
