using PLCStationInterfaceWPF.Classes;
using PLCStationInterfaceWPF.JDO;
using System;
using System.Windows;
using System.Windows.Controls;
using WPFUtilsLib.Helpers;
using WPFUtilsLib.MessageBoxes;
using WPFUtilsLib.Net;
using WPFUtilsLib.TCPIP;

namespace PLCStationInterfaceWPF.Windows.Settings
{
    public partial class StationTCPServerSettings : Page
    {
        private TCPServerSettingsJDO _TCPServerSettings;
        private Server _server;

        private string ErrorMessageBoxTitle = "";
        private string[] Errors = new string[1];

        private string MessageMessageBoxTitle = "";
        private string Message = "";

        public StationTCPServerSettings(TCPServerSettingsJDO tcpServerSettings, Server server)
        {
            InitializeComponent();

            _TCPServerSettings = tcpServerSettings;
            _server = server;

            //LoginBox = loginBox;

            SetJSONDataToContols(ref _TCPServerSettings);
            //SetControlEnables(true);

            _server.StatusChanged += ServerStatus_Changed;
            Translator.LanguageChanged += Translate;

        }

        private void Translate(object sender, EnumLanguage e)
        {
            if (Translator.Language == EnumLanguage.CZ)
            {
                lblTitle.Text = "Nastavení Databáze";
                lblIPAddress.Text = "IP Adresa:";
                btnConnect.Content = "Připojit";
                btnDisconnect.Content = "Odpojit";
                btnApply.Content = "Použít";

                ErrorMessageBoxTitle = "Chyba uživatelského vstupu";

                Errors[0] = "IP Adresa není ve správném tvaru. Např. 192.168.1.1";

                MessageMessageBoxTitle = "Zpráva";

                Message = "Data byla úspěšně uložena.";
            }
            else if (Translator.Language == EnumLanguage.ENG)
            {
                lblTitle.Text = "Database Settings";
                lblIPAddress.Text = "IP Address:";
                btnConnect.Content = "Connect";
                btnDisconnect.Content = "Disconnect";
                btnApply.Content = "Apply";

                ErrorMessageBoxTitle = "User Input Error";

                Errors[0] = "IP Address is not in valide format. Eg. 192.168.1.1";

                MessageMessageBoxTitle = "Message";

                Message = "Data was be correctly seved";
            }
        }

        private void ServerStatus_Changed(object sender, ServerStatus e)
        {
            if (e.Equals(ServerStatus.Running))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
            }
            else if (e.Equals(ServerStatus.Stopped))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
            }
        }

        private void SetJSONDataToContols(ref TCPServerSettingsJDO Settings)
        {
            ipab.IPAddress = Settings.IPAddress;
            tbServerPort.Value = Settings.Port;

        }

        private void SetControlsDataToJASON(ref TCPServerSettingsJDO Settings)
        {
            Settings.IPAddress = ipab.IPAddress;
            Settings.Port = tbServerPort.Value;
        }

        private void SetParametrToDBAndConnect(ref Server server, ref TCPServerSettingsJDO Settings, bool Connect)
        {
            server.IPAddress = Settings.IPAddress;
            server.Port = (ushort)Settings.Port;

            if (Connect == false) return;

#pragma warning disable CS4014 
            server.StartAsync();
#pragma warning restore CS4014
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_server.Status != ServerStatus.Stopped) return;

            SetParametrToDBAndConnect(ref _server, ref _TCPServerSettings, true);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_server.Status == ServerStatus.Stopped) return;
            _server.Stop();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (ipab.IPAddressValid == false)
            {
                CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[0]);
                return;
            }

            SetControlsDataToJASON(ref _TCPServerSettings);

            CustomMessageBox.ShowPopup(MessageMessageBoxTitle, Message);

            SetParametrToDBAndConnect(ref _server, ref _TCPServerSettings, false);
        }

        private void pStationTCPIPServer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetJSONDataToContols(ref _TCPServerSettings);
        }
    }
}
