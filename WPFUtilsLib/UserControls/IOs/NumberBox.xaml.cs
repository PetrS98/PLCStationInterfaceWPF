using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtilsLib.UserControls.IOs
{
    public partial class NumberBox : TextBox
    {
        private int _value = 0;
        private int _minimum = 0;
        private int _maximum = int.MaxValue;

        public event EventHandler<int> ValueChanged;
        public event EventHandler<int> MinimumChanged;
        public event EventHandler<int> MaximumChanged;

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
                Text = value.ToString();
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

            foreach (char c in Text)
            {
                if (c > 47 && c < 58) stringBuilder.Append(c);
            }

            bool parseable = int.TryParse(stringBuilder.ToString(), out int value);
            return parseable ? value : 0;
        }

        private void UpdateValue(object sender, int e)
        {
            Value = ParseValue();
            Select(Text.Length, 0);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue(sender, _value);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Text = Value.ToString();
        }
    }
}
