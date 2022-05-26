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
        private string[] Errors = new string[7];

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

            //MySQLDatabase.StatusChanged += DBStatusChange;
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
                Errors[1] = "Jméno databáze není ve správném tvaru nebo obsahuje nepovolené znaky. Např. Databaze_";
                Errors[2] = "Jméno tabulky s uživateli není ve správném tvaru nebo obsahuje nepovolené znaky. Např. Tabulka_";
                Errors[3] = "SPARE";
                Errors[4] = "Jméno tabulky s daty prostojů není ve správném tvaru nebo obsahuje nepovolené znaky. Např.Tabulka_";
                Errors[5] = "Uživatelské jméno pro připojení k databázi není ve správném tvaru nebo obsahuje nepovolené znaky. Např. User1";
                Errors[6] = "Heslo k databázi nesmí být prázdné.";

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
                Errors[1] = "Database name is not in correct format or contains illegal characters. Eg. Database_";
                Errors[2] = "Users Table name is not in correct format or contains illegal characters. Eg. Table_";
                Errors[3] = "SPARE";
                Errors[4] = "NonOPs Data Table name is not in correct format or contains illegal characters. Eg. Table_";
                Errors[5] = "Database user name is not in correct format or contains illegal characters. Eg. Table_";
                Errors[6] = "Database password must not be empty.";

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

        private void btnApply_Click(object sender, EventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            //if (ipAddressBox.IPAddressValid)
            //{
            //    Settings.DatabaseSettings.IPAddress = ipAddressBox.IPAddress;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[0]);
            //    return;
            //}

            //if (TextBoxHelper.TbInputIsText(tbDatabaseName))
            //{
            //    Settings.DatabaseSettings.DatabaseName = tbDatabaseName.Text;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[1]);
            //    return;
            //}

            //if (TextBoxHelper.TbInputIsText(tbUsersTableName))
            //{
            //    Settings.DatabaseSettings.UsersTableName = tbUsersTableName.Text;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[2]);
            //    return;
            //}

            //if (TextBoxHelper.TbInputIsText(tbNonOPsDataTableName))
            //{
            //    Settings.DatabaseSettings.NonOPsDataTableName = tbNonOPsDataTableName.Text;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[4]);
            //    return;
            //}

            //if (TextBoxHelper.TbInputIsText(tbDatabaseUserName))
            //{
            //    Settings.DatabaseSettings.DatabaseUserName = tbDatabaseUserName.Text;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[5]);
            //    return;
            //}

            //if (tbDatabasePassword.Text != null && tbDatabasePassword.Text != "")
            //{
            //    Settings.DatabaseSettings.DatabasePassword = tbDatabasePassword.Text;
            //}
            //else
            //{
            //    CustomMessageBox.ShowPopup(ErrorMessageBoxTitle, Errors[6]);
            //    return;
            //}

            CustomMessageBox.ShowPopup(MessageMessageBoxTitle, Message);

            //MySQLDatabase.DatabaseName = Settings.DatabaseSettings.DatabaseName;
            //MySQLDatabase.IPAddress = Settings.DatabaseSettings.IPAddress;
            //MySQLDatabase.UserName = Settings.DatabaseSettings.DatabaseUserName;
            //MySQLDatabase.Password = Settings.DatabaseSettings.DatabasePassword;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            //if (MySQLDatabase.Status != ClientStatus.Disconnected) return;

            //MySQLDatabase.DatabaseName = Settings.DatabaseSettings.DatabaseName;
            //MySQLDatabase.IPAddress = Settings.DatabaseSettings.IPAddress;
            //MySQLDatabase.UserName = Settings.DatabaseSettings.DatabaseUserName;
            //MySQLDatabase.Password = Settings.DatabaseSettings.DatabasePassword;
            //MySQLDatabase.ConnectToDB_Async();

            //MySQLDatabase.ConnectToDB(Settings.DatabaseSettings.IPAddress, Settings.DatabaseSettings.DatabaseUserName, Settings.DatabaseSettings.DatabasePassword);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            //if (MySQLDatabase.Equals(ClientStatus.Connected)) return;
            //MySQLDatabase.DisconnectFromDB(true);
        }

        private void DatabaseSettings_VisibleChanged(object sender, EventArgs e)
        {
            SetJSONDataToContols();
        }

        private void SetJSONDataToContols()
        {
            //ipAddressBox.IPAddress = Settings.DatabaseSettings.IPAddress;
            //tbDatabaseName.Text = Settings.DatabaseSettings.DatabaseName;
            //tbUsersTableName.Text = Settings.DatabaseSettings.UsersTableName;
            //tbNonOPsDataTableName.Text = Settings.DatabaseSettings.NonOPsDataTableName;
            //tbDatabaseUserName.Text = Settings.DatabaseSettings.DatabaseUserName;
            //tbDatabasePassword.Text = Settings.DatabaseSettings.DatabasePassword;
        }

        private void SetControlEnables(bool Enable)
        {
            btnConnect.IsEnabled = Enable;
            btnDisconnect.IsEnabled = !Enable;
        }
    }
}
