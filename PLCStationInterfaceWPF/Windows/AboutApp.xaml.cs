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
using System.Diagnostics;

namespace PLCStationInterfaceWPF.Windows
{
    /// <summary>
    /// Interakční logika pro AboutApp.xaml
    /// </summary>
    public partial class AboutApp : Page
    {
        public AboutApp()
        {
            InitializeComponent();
        }

        private void GoToWebSide(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.nexentire.com/cz/") { UseShellExecute = true });
        }
    }
}
