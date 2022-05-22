using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilsLib.UserControls.IOs
{
    public partial class IPAddressBox : UserControl
    {
        private static readonly Brush ForegroundValid = new SolidColorBrush(Color.FromRgb(0, 128, 0));
        private static readonly Brush ForegroundInvalid = new SolidColorBrush(Color.FromRgb(192, 0, 0));
        private static readonly Regex _ipRegex = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

        #region TextBox Properties


        public new FontStyle FontStyle { get { return textBox.FontStyle; } set { textBox.FontStyle = value; } }
        public new double FontSize { get { return textBox.FontSize; } set { textBox.FontSize = value; } }
        public new FontWeight FontWeight { get { return textBox.FontWeight; } set { textBox.FontWeight = value; } }
        public new FontFamily FontFamily { get { return textBox.FontFamily; } set { textBox.FontFamily = value; } }
        public bool Enable { get { return textBox.IsEnabled; } set { textBox.IsEnabled = value; } }
        public bool ReadOnly { get { return textBox.IsReadOnly; } set { textBox.IsReadOnly = value; } }

        #endregion

        private bool ipAddressValid = false;
        public bool IPAddressValid
        {
            get => ipAddressValid;
            private set
            {
                ipAddressValid = value;
                textBox.Foreground = IPAddressValid ? ForegroundValid : ForegroundInvalid;
            }
        }

        public string IPAddress
        {
            get => textBox.Text;
            set
            {
                textBox.Text = value;
                IPAddressValid = _ipRegex.IsMatch(value);
            }
        }

        public IPAddressBox()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IPAddress = textBox.Text;
        }
    }
}
