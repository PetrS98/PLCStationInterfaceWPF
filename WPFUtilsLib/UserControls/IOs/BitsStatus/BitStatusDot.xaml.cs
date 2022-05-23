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
    public partial class BitStatusDot : UserControl
    {
        private static readonly ImageSource GreenDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/GreenDot.png"));
        private static readonly ImageSource WhiteDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/WhiteDot.png"));

        private bool status;

        public bool Status
        {
            get { return status; }
            set
            {
                status = value;

                if (status) img.Source = GreenDot;
                else img.Source = WhiteDot;
            }
        }

        public BitStatusDot()
        {
            InitializeComponent();
        }
    }
}
