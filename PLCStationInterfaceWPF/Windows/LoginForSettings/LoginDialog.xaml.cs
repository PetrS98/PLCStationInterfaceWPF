using PLCStationInterfaceWPF.Classes;
using PLCStationInterfaceWPF.JDO.SettingsLogin;
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
using System.Windows.Shapes;
using WPFUtilsLib.MessageBoxes;
using WPFUtilsLib.Services.Enums;

namespace PLCStationInterfaceWPF.Windows.LoginForSettings
{
    public partial class LoginDialog : Window
    {
        public string LoginUser { get; set; } = "";

        SettingsLoginJDO SettingsLogin;

        private string Error = "";

        public event EventHandler<bool> LogedChanged;

        private bool logedIn;

        public bool LogedIn
        {
            get
            {
                return logedIn;
            }
            set
            {
                if (logedIn != value) LogedChanged?.Invoke(null, value);

                logedIn = value;
            }
        }

        public LoginDialog(SettingsLoginJDO settingsLogin)
        {
            InitializeComponent();

            TopBar.Window = this;
            TopBar.ClosingAction = ClosingAction.CloseWindow;

            Translator.LanguageChanged += Translate;

            SettingsLogin = settingsLogin;
        }

        private void Translate(object sender, EnumLanguage e)
        {
            if (Translator.Language == EnumLanguage.CZ)
            {
                //lblUserName.Text = "Uživatelské Jméno:";
                //lblPassword.Text = "Heslo:";
                //btnLogin.Text = "Přihlásit";
                //btnCancel.Text = "Zrušit";

                Error = "Uživatelské jméno nebo heslo není správné!";
            }
            else if (Translator.Language == EnumLanguage.ENG)
            {
                //lblUserName.Text = "User Name:";
                //lblPassword.Text = "Password:";
                //btnLogin.Text = "Login";
                //btnCancel.Text = "Cancel";

                Error = "User name or password is incorrect!";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < SettingsLogin.UserNames.Length; i++)
            {
                if (tbUserName.Text == SettingsLogin.UserNames[i] && tbPassword.Password == SettingsLogin.Passwords[i])
                {
                    LoginUser = SettingsLogin.UserNames[i];
                    LogedIn = true;
                    break;
                }
            }

            if (LogedIn == true)
            {
                HideLoginDialog(false);
            }
            else
            {
                CustomMessageBox.ShowPopup("Login Error", Error);
                return;
            }
        }

        public void HideLoginDialog(bool LogOff)
        {
            tbPassword.Password = "";
            tbUserName.Text = "";

            if (IsVisible == false) return;

            Hide();

            if (LogOff == false && LogedIn == true) return;

            LogedIn = false;
            LoginUser = "";
        }

        public bool CheckLogin()
        {
            if (LogedIn == false)
            {
                ShowLoginDialog();
                return false;
            }

            return true;
        }

        public void ShowLoginDialog()
        {
            if (IsVisible == true) return;
            Show();
        }
    }
}
