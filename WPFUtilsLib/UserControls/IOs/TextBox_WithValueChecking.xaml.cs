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

namespace WPFUtilsLib.UserControls.IOs
{
    public enum Mode { None, IsOnlyNumber, ViaStringMask }

    public partial class TextBox_WithValueChecking : UserControl
    {
        public string CheckMask { get; set; }
        public bool InputIsOK { get; private set; }

        public string Text
        {
            get 
            {
                InputIsOK = CheckInput();
                return textBox.Text; 
            }
            set 
            { 
                textBox.Text = value;
            }
        }


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
        public Mode CheckMode { get; set; }

        #endregion

        public TextBox_WithValueChecking()
        {
            InitializeComponent();
        }

        private bool CheckInput()
        {
            if (CheckMode == Mode.None) return true;

            if(CheckMode == Mode.IsOnlyNumber)
            {
                return TbInputIsNumber(textBox);
            }
            else
            {
                if (CheckMask == null) return false;
                return TbInputIsText(textBox);
            }
        }

        public bool TbInputIsNumber(TextBox textBox)
        {
            return int.TryParse(textBox.Text, out int result);
        }

        public bool TbInputIsText(TextBox textBox)
        {
            if (textBox.Text == null) return false;

            for (int i = 0; i < textBox.Text.Length; i++)
            { 
                if (CheckInMask(textBox.Text[i], CheckMask)) continue;
                return false;  
            }

            return true;
        }
        private bool CheckInMask(char c, string Mask)
        {
            return Mask.IndexOf(c) != -1;
        }
    }
}
