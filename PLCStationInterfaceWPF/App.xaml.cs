using PLCStationInterfaceWPF.Windows;
using PLCStationInterfaceWPF.Windows.Settings;
using PLCStationInterfaceWPF.Windows.Testing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PLCStationInterfaceWPF
{
    public partial class App : Application
    {
        private readonly MainMenu _mainMenu;

        private readonly PLCSettings _plcSettings;
        private readonly StationTCPServerSettings _stationTCPServerSettings;
        private readonly AboutApp _aboutApp;
        private readonly Diagnostics _diagnostics;
        private readonly InterfaceData _interfaceData;
        private readonly DatabaseSettings _databaseSettings;

        public App()
        {
            _plcSettings = new PLCSettings();
            _stationTCPServerSettings = new StationTCPServerSettings();
            _aboutApp = new AboutApp();
            _diagnostics = new Diagnostics();
            _interfaceData = new InterfaceData();
            _databaseSettings = new DatabaseSettings();

            _mainMenu = new MainMenu(_plcSettings, _stationTCPServerSettings, _aboutApp, _diagnostics, _interfaceData, _databaseSettings);
            _mainMenu.Show();

            Test tst = new Test();
            tst.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}
