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
using WPFUtilsLib.Services;
using PLCStationInterfaceWPF.Classes;
using WPFUtilsLib.Net;
using WPFUtilsLib.Helpers;
using WPFUtilsLib.MessageBoxes;
using PLCStationInterfaceWPF.JDO;

namespace PLCStationInterfaceWPF.Windows.Settings
{
    public partial class DatabaseSettings : Page
    {
        DatabaseSettingsJDO _DatabaseSettings;
        private MySQLDatabase _mySQLDatabase;

        private string ErrorMessageBoxTitle = "";
        private string[] Errors = new string[1];

        private string MessageMessageBoxTitle = "";
        private string Message = "";

        public DatabaseSettings(DatabaseSettingsJDO databaseSettings, MySQLDatabase mySQLDatabase)
        {
            InitializeComponent();

            _DatabaseSettings = databaseSettings;
            _mySQLDatabase = mySQLDatabase;

            //LoginBox = loginBox;

            SetJSONDataToContols();
            SetControlEnables(true);

            _mySQLDatabase.StatusChanged += DBStatusChange;
            Translator.LanguageChanged += Translate;

        }

        private void Translate(object sender, EnumLanguage e)
        {
            if (Translator.Language == EnumLanguage.CZ)
            {
                lblTitle.Text = "Nastavení Databáze";
                lblIPAddress.Text = "IP Adresa:";
                lblDatabaseName.Text = "Jméno Databáze:";
                lblNonOpTextTable.Text = "Jméno Tabulky s Prostoji:";
                lblDatabaseUserName.Text = "Uživatelské Jméno k Databázi:";
                lblDatabasePassword.Text = "Heslo k Databázi:";
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
                lblDatabaseName.Text = "Database Name:";
                lblNonOpTextTable.Text = "NonOperation Table Name:";
                lblDatabaseUserName.Text = "Database User Name:";
                lblDatabasePassword.Text = "Database Password:";
                btnConnect.Content = "Connect";
                btnDisconnect.Content = "Disconnect";
                btnApply.Content = "Apply";

                ErrorMessageBoxTitle = "User Input Error";

                Errors[0] = "IP Address is not in valide format. Eg. 192.168.1.1";
                
                MessageMessageBoxTitle = "Message";

                Message = "Data was be correctly seved";
            }
        }

        private void DBStatusChange(object sender, ClientStatus e)
        {
            if (e.Equals(ClientStatus.Connected))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
            }
            else if (e.Equals(ClientStatus.Disconnected))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
            }
        }

        private void DatabaseSettings_VisibleChanged(object sender, EventArgs e)
        {
            SetJSONDataToContols();
        }

        private void SetJSONDataToContols()
        {
            ipab.IPAddress = _DatabaseSettings.IPAddress;
            tbDatabaseName.Text = _DatabaseSettings.DatabaseName;
            tbNonOpTextTable.Text = _DatabaseSettings.NonOPMessageTable;
            tbDatabaseUserName.Text = _DatabaseSettings.DatabaseUserName;
            tbDatabasePassword.Password = _DatabaseSettings.DatabasePassword;
        }

        private void SetControlsDataToJASON()
        {
            _DatabaseSettings.IPAddress = ipab.IPAddress;
            _DatabaseSettings.DatabaseName = tbDatabaseName.Text;
            _DatabaseSettings.NonOPMessageTable = tbNonOpTextTable.Text;
            _DatabaseSettings.DatabaseUserName = tbDatabaseUserName.Text;
            _DatabaseSettings.DatabasePassword = tbDatabasePassword.Password;
        }

        private void SetParametrToDBAndConnect(bool Connect)
        {
            _mySQLDatabase.DatabaseName = _DatabaseSettings.DatabaseName;
            _mySQLDatabase.IPAddress = _DatabaseSettings.IPAddress;
            _mySQLDatabase.UserName = _DatabaseSettings.DatabaseUserName;
            _mySQLDatabase.Password = _DatabaseSettings.DatabasePassword;

            if (Connect == false) return;
            _mySQLDatabase.ConnectToDB_Async();
        }

        private void SetControlEnables(bool Enable)
        {
            btnConnect.IsEnabled = Enable;
            btnDisconnect.IsEnabled = !Enable;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_mySQLDatabase.Status != ClientStatus.Disconnected) return;

            SetParametrToDBAndConnect(true);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_mySQLDatabase.Equals(ClientStatus.Connected)) return;
            _mySQLDatabase.DisconnectFromDB(true);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (ipab.IPAddressValid == false)
            {
                CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[0]);
                return;
            }

            SetControlsDataToJASON();

            CustomMessageBox.ShowPopup(MessageMessageBoxTitle, Message);

            SetParametrToDBAndConnect(false);
        }
    }
}
