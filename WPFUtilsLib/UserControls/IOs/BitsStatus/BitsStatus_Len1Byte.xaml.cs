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
    public partial class BitsStatus_Len1Byte : UserControl
    {
        public string Text_Sts1 { get { return bs_1.Text; } set { bs_1.Text = value; } }
        public Brush BackColor_Sts1 { get { return bs_1.Background; } set { bs_1.Background = value; } }
        public Brush ForeColor_Sts1 { get { return bs_1.Foreground; } set { bs_1.Foreground = value; } }
        public FontWeight FontWeight_Sts1 { get { return bs_1.FontWeight; } set { bs_1.FontWeight = value; } }
        public Style Style_Sts1 { get { return bs_1.Style; } set { bs_1.Style = value; } }

        public string Text_Sts2 { get { return bs_2.Text; } set { bs_2.Text = value; } }
        public Brush BackColor_Sts2 { get { return bs_2.Background; } set { bs_2.Background = value; } }
        public Brush ForeColor_Sts2 { get { return bs_2.Foreground; } set { bs_2.Foreground = value; } }
        public FontWeight FontWeight_Sts2 { get { return bs_2.FontWeight; } set { bs_2.FontWeight = value; } }
        public Style Style_Sts2 { get { return bs_2.Style; } set { bs_2.Style = value; } }

        public string Text_Sts3 { get { return bs_3.Text; } set { bs_3.Text = value; } }
        public Brush BackColor_Sts3 { get { return bs_3.Background; } set { bs_3.Background = value; } }
        public Brush ForeColor_Sts3 { get { return bs_3.Foreground; } set { bs_3.Foreground = value; } }
        public FontWeight FontWeight_Sts3 { get { return bs_3.FontWeight; } set { bs_3.FontWeight = value; } }
        public Style Style_Sts3 { get { return bs_3.Style; } set { bs_3.Style = value; } }

        public string Text_Sts4 { get { return bs_4.Text; } set { bs_4.Text = value; } }
        public Brush BackColor_Sts4 { get { return bs_4.Background; } set { bs_4.Background = value; } }
        public Brush ForeColor_Sts4 { get { return bs_4.Foreground; } set { bs_4.Foreground = value; } }
        public FontWeight FontWeight_Sts4 { get { return bs_3.FontWeight; } set { bs_4.FontWeight = value; } }
        public Style Style_Sts4 { get { return bs_4.Style; } set { bs_4.Style = value; } }

        public string Text_Sts5 { get { return bs_5.Text; } set { bs_5.Text = value; } }
        public Brush BackColor_Sts5 { get { return bs_5.Background; } set { bs_5.Background = value; } }
        public Brush ForeColor_Sts5 { get { return bs_5.Foreground; } set { bs_5.Foreground = value; } }
        public FontWeight FontWeight_Sts5 { get { return bs_5.FontWeight; } set { bs_5.FontWeight = value; } }
        public Style Style_Sts5 { get { return bs_5.Style; } set { bs_5.Style = value; } }

        public string Text_Sts6 { get { return bs_6.Text; } set { bs_6.Text = value; } }
        public Brush BackColor_Sts6 { get { return bs_6.Background; } set { bs_6.Background = value; } }
        public Brush ForeColor_Sts6 { get { return bs_6.Foreground; } set { bs_6.Foreground = value; } }
        public FontWeight FontWeight_Sts6 { get { return bs_6.FontWeight; } set { bs_6.FontWeight = value; } }
        public Style Style_Sts6 { get { return bs_6.Style; } set { bs_6.Style = value; } }

        public string Text_Sts7 { get { return bs_7.Text; } set { bs_7.Text = value; } }
        public Brush BackColor_Sts7 { get { return bs_7.Background; } set { bs_7.Background = value; } }
        public Brush ForeColor_Sts7 { get { return bs_7.Foreground; } set { bs_7.Foreground = value; } }
        public FontWeight FontWeight_Sts7 { get { return bs_7.FontWeight; } set { bs_7.FontWeight = value; } }
        public Style Style_Sts7 { get { return bs_7.Style; } set { bs_7.Style = value; } }

        public string Text_Sts8 { get { return bs_8.Text; } set { bs_8.Text = value; } }
        public Brush BackColor_Sts8 { get { return bs_8.Background; } set { bs_8.Background = value; } }
        public Brush ForeColor_Sts8 { get { return bs_8.Foreground; } set { bs_8.Foreground = value; } }
        public FontWeight FontWeight_Sts8 { get { return bs_8.FontWeight; } set { bs_8.FontWeight = value; } }
        public Style Style_Sts8 { get { return bs_8.Style; } set { bs_8.Style = value; } }

        public bool BorederON 
        {
            set 
            {
                for (int i = 0; i < BorderStatusBit.Length; i++)
                {
                    if (value == true)
                    {
                        BorderStatusBit[i].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        BorderStatusBit[i].Visibility = Visibility.Hidden;
                    }
                    
                }
            } 
        }

        public new FontStyle FontStyle 
        { get { return bs_8.FontStyle; } 
          set 
            { 
                bs_1.FontStyle = value;
                bs_2.FontStyle = value;
                bs_3.FontStyle = value;
                bs_4.FontStyle = value;
                bs_5.FontStyle = value;
                bs_6.FontStyle = value;
                bs_7.FontStyle = value;
                bs_8.FontStyle = value;
            } 
        }

        public new double FontSize 
        { get { return bs_8.FontSize; } 
          set 
            {
                bs_1.FontSize = value;
                bs_2.FontSize = value;
                bs_3.FontSize = value;
                bs_4.FontSize = value;
                bs_5.FontSize = value;
                bs_6.FontSize = value;
                bs_7.FontSize = value;
                bs_8.FontSize = value;
            } 
        }
        public new FontFamily FontFamily 
        { get { return bs_8.FontFamily; } 
          set 
            {
                bs_1.FontFamily = value;
                bs_2.FontFamily = value;
                bs_3.FontFamily = value;
                bs_4.FontFamily = value;
                bs_5.FontFamily = value;
                bs_6.FontFamily = value;
                bs_7.FontFamily = value;
                bs_8.FontFamily = value;
            } 
        }

        BitStatus[] BitStatusControl;
        Border[] BorderStatusBit;

        public BitsStatus_Len1Byte()
        {
            InitializeComponent();
            BitStatusControl = new BitStatus[] { bs_1, bs_2, bs_3, bs_4, bs_5, bs_6, bs_7, bs_8 };
            BorderStatusBit = new Border[] { b1, b2, b3, b4, b5, b6, b7, b8 };
        }

        public bool SetStatus(ushort Index, bool Value)
        {
            if (Index >= BitStatusControl.Length - 1) return false;

            BitStatusControl[Index].Status = Value;
            return true;
        }
    }
}
