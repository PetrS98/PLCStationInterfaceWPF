using PLCStationInterfaceWPF.Classes;
using PLCStationInterfaceWPF.UDT.InterfaceData;
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

namespace PLCStationInterfaceWPF.Windows
{
    /// <summary>
    /// Interakční logika pro InterfaceData.xaml
    /// </summary>
    public partial class InterfaceData : Page
    {
        public Brush ActiveButtonColor { get; set; } = new SolidColorBrush(Color.FromRgb(128, 0, 128));
        public Brush InactiveSubmenuButtonColor { get; set; } = new SolidColorBrush(Color.FromRgb(80, 80, 80));

        private readonly Dictionary<Button, int> buttons = new Dictionary<Button, int>();

        PLCTCPServerDataHandler _plcTCPServerDataHandler;

        private Button _activeButton = null;
        public Button ActiveButton
        {
            get { return _activeButton; }
            set
            {
                if (_activeButton != null)
                {
                    _activeButton.Background = InactiveSubmenuButtonColor;
                }

                _activeButton = value;

                _activeButton.Background = ActiveButtonColor;
            }
        }

        public int ActiveStation { get; set; }

        public InterfaceData(PLCTCPServerDataHandler plcTCPServerDataHandler)
        {
            InitializeComponent();

            buttons.Add(btnStation1, 0);
            buttons.Add(btnStation2, 1);
            buttons.Add(btnStation3, 2);
            buttons.Add(btnStation4, 3);

            ActiveStation = 0;

            _plcTCPServerDataHandler = plcTCPServerDataHandler;

            _plcTCPServerDataHandler.InterfaceDataChanged += InterfaceData_Changed;
        }

        private void InterfaceData_Changed(object sender, InterfaceDataUDT e)
        {
            //throw new NotImplementedException();
        }

        private void StationButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton = sender as Button;
            ActiveStation = buttons[ActiveButton];
        }
    }
}
