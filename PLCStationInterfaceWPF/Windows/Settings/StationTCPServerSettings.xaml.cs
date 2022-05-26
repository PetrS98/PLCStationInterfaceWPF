using PLCStationInterfaceWPF.JDO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUtilsLib.TCPIP;

namespace PLCStationInterfaceWPF.Windows.Settings
{
    public partial class StationTCPServerSettings : Page
    {
        private TCPServerSettingsJDO _TCPServerSettings;
        private Server _server;

        public StationTCPServerSettings(TCPServerSettingsJDO tcpServerSettings, Server server)
        {
            InitializeComponent();
            _TCPServerSettings = tcpServerSettings;
            _server = server;

        }
    }
}
