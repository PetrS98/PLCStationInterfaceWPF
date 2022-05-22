using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilsLib.UserControls.IOs
{
    public partial class NumberBox : UserControl
    {
        private int _value = 0;
        private int _minimum = 0;
        private int _maximum = int.MaxValue;

        public event EventHandler<int> ValueChanged;
        public event EventHandler<int> MinimumChanged;
        public event EventHandler<int> MaximumChanged;

        #region TextBox Properties

        public Brush BackColor { get { return textBox.Background; } set { textBox.Background = value; } }
        public Brush ForeColor { get { return textBox.Foreground; } set { textBox.Foreground = value; } }
        public new FontStyle FontStyle { get { return textBox.FontStyle; } set { textBox.FontStyle = value; } }
        public new double FontSize { get { return textBox.FontSize; } set { textBox.FontSize = value; } }
        public new FontWeight FontWeight { get { return textBox.FontWeight; } set { textBox.FontWeight = value; } }
        public new FontFamily FontFamily { get { return textBox.FontFamily; } set { textBox.FontFamily = value; } }
        public bool Enable { get { return textBox.IsEnabled; } set { textBox.IsEnabled = value; } }
        public bool ReadOnly { get { return textBox.IsReadOnly; } set { textBox.IsReadOnly = value; } }
        public Brush ThicknesBrush { get { return textBox.BorderBrush; } set { textBox.BorderBrush = value; } }
        public new Thickness BorderThickness { get { return textBox.BorderThickness; } set { textBox.BorderThickness = value; } }
        public new Style Style { get { return textBox.Style; } set { textBox.Style = value; } }

        #endregion

        public int Value
        {
            get
            {
                int v = _value < Minimum ? Minimum : _value;
                return v > Maximum ? Maximum : v;
            }
            set
            {
                bool changed = _value != value;
                _value = value;
                textBox.Text = value.ToString();
                if (changed) ValueChanged?.Invoke(this, value);
            }
        }
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (_minimum == value) return;
                if (value > Maximum) throw new ArgumentException("Minimum cannot be greater than Maximum");

                _minimum = value;
                MinimumChanged?.Invoke(this, value);
            }
        }
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (_maximum == value) return;
                if (value < Minimum) throw new ArgumentException("Maximum cannot be less than Minimum");

                _maximum = value;
                MaximumChanged?.Invoke(this, value);
            }
        }

        public NumberBox()
        {
            InitializeComponent();

            ValueChanged += UpdateValue;
            MinimumChanged += UpdateValue;
            MaximumChanged += UpdateValue;
        }

        private int ParseValue()
        {
            StringBuilder stringBuilder = new StringBuilder("");

            foreach (char c in textBox.Text)
            {
                if (c > 47 && c < 58) stringBuilder.Append(c);
            }

            bool parseable = int.TryParse(stringBuilder.ToString(), out int value);
            return parseable ? value : 0;
        }

        private void UpdateValue(object sender, int e)
        {
            Value = ParseValue();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue(sender, _value);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = Value.ToString();
        }
    }
}
