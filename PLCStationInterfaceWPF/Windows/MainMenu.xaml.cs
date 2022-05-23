using PLCStationInterfaceWPF.Windows;
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
using WPFUtilsLib.UserControls.IOs;

namespace PLCStationInterfaceWPF
{
    public partial class MainMenu : Window
    {
        public Brush ActiveButtonColor { get; set; } = new SolidColorBrush(Color.FromRgb(128, 0, 128));
        public Brush InactiveSubmenuButtonColor { get; set; } = new SolidColorBrush(Color.FromRgb(80, 80, 80));
        public Brush InactiveButtonColor { get; set; } = new SolidColorBrush(Color.FromRgb(64, 64, 64));

        private readonly Dictionary<Button, Page> _pages = new Dictionary<Button, Page>();

        public Page ActivePage
        {
            get => pageFrame.Content as Page;
            private set => pageFrame.Content = value;
        }

        private Button _activeButton = null;
        public Button ActiveButton
        {
            get => _activeButton;
            private set
            {
                if (!(_activeButton is null))
                {
                    _activeButton.Background = InactiveButtonColor;
                }

                _activeButton = value;

                if (value is null)
                {
                    ActivePage = null;
                }
                else
                {
                    _activeButton.Background = ActiveButtonColor;
                    ActivePage = _pages[value];
                }
            }
        }

        public MainMenu(PLCSettings plcSettings, StationTCPServerSettings stationTCPServerSettings, AboutApp aboutApp, Diagnostics diagnostics, InterfaceData interfaceData)
        {
            InitializeComponent();

            TopBar.Window = this;
            TopBar.ClosingAction = ClosingAction.CloseApp;

            AddMenuEntry(btnPLCSettings, plcSettings);
            AddMenuEntry(btnStationTCPServerSettings, stationTCPServerSettings);
            AddMenuEntry(btnAboutApp, aboutApp);
            AddMenuEntry(btnDiagnostics, diagnostics);
            AddMenuEntry(btnStationInterfaceDataStatus, interfaceData);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddMenuEntry(Button button, Page page)
        {
            button.Click += (sender, e) => ActiveButton = button;
            _pages.Add(button, page);
        }
    }
}
