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

namespace WPFUtilsLib.UserControls.IOs.BitsStatus
{
    /// <summary>
    /// Interakční logika pro BitStatus.xaml
    /// </summary>
    public partial class BitStatus : UserControl
    {
        public string Text { get { return textBlock.Text; } set { textBlock.Text = value; } }
        public Brush BackColor { get { return textBlock.Background; } set { textBlock.Background = value; } }
        public Brush ForeColor { get { return textBlock.Foreground; } set { textBlock.Foreground = value; } }
        public new FontStyle FontStyle { get { return textBlock.FontStyle; } set { textBlock.FontStyle = value; } }
        public new double FontSize { get { return textBlock.FontSize; } set { textBlock.FontSize = value; } }
        public new FontWeight FontWeight { get { return textBlock.FontWeight; } set { textBlock.FontWeight = value; } }
        public new FontFamily FontFamily { get { return textBlock.FontFamily; } set { textBlock.FontFamily = value; } }
        public new Style Style { get { return textBlock.Style; } set { textBlock.Style = value; } }
        public bool Status { get { return bsd.Status; } set { bsd.Status = value; } }

        public BitStatus()
        {
            InitializeComponent();
        }
    }
}
