using PLCStationInterfaceWPF.Windows.Settings;
using PLCStationInterfaceWPF.Windows.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLCStationInterfaceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        Test test = new Test();
        PLCSettings PLCSettings = new PLCSettings();
        public MainMenu()
        {
            InitializeComponent();
            TopBar.Window = this;

            test.Show();
            test.Topmost = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //if (PLCSettings.Visibility == Visibility.Visible) return;

            pageFrame.Content = PLCSettings;
        }
    }
}
