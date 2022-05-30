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

            SetJSONDataToContols(ref _DatabaseSettings);
            //SetControlEnables(true);

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

        private void SetJSONDataToContols(ref DatabaseSettingsJDO Settings)
        {
            ipab.IPAddress = Settings.IPAddress;
            tbDatabaseName.Text = Settings.DatabaseName;
            tbNonOpTextTable.Text = Settings.NonOPMessageTable;
            tbDatabaseUserName.Text = Settings.DatabaseUserName;
            tbDatabasePassword.Password = Settings.DatabasePassword;
        }

        private void SetControlsDataToJASON(ref DatabaseSettingsJDO Settings)
        {
            Settings.IPAddress = ipab.IPAddress;
            Settings.DatabaseName = tbDatabaseName.Text;
            Settings.NonOPMessageTable = tbNonOpTextTable.Text;
            Settings.DatabaseUserName = tbDatabaseUserName.Text;
            Settings.DatabasePassword = tbDatabasePassword.Password;
        }

        private void SetParametrToDBAndConnect(ref MySQLDatabase DB, ref DatabaseSettingsJDO Settings, bool Connect)
        {
            DB.DatabaseName = Settings.DatabaseName;
            DB.IPAddress = Settings.IPAddress;
            DB.UserName = Settings.DatabaseUserName;
            DB.Password = Settings.DatabasePassword;

            if (_mySQLDatabase.Status != ClientStatus.Disconnected) return;
            if (Connect == false) return;

            DB.ConnectToDB_Async();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_mySQLDatabase.Status != ClientStatus.Disconnected) return;

            SetParametrToDBAndConnect(ref _mySQLDatabase, ref _DatabaseSettings, true);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if (_mySQLDatabase.Status == ClientStatus.Disconnected) return;
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

            SetControlsDataToJASON(ref _DatabaseSettings);

            CustomMessageBox.ShowPopup(MessageMessageBoxTitle, Message);

            SetParametrToDBAndConnect(ref _mySQLDatabase, ref _DatabaseSettings, false);
        }

        private void pDatabaseSettings_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetJSONDataToContols(ref _DatabaseSettings);
        }
    }
}
