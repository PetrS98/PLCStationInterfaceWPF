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
using System.Windows.Shapes;
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.MessageBoxes
{
    /// <summary>
    /// Interakční logika pro CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
            TopBar.Window = this;
            TopBar.ClosingAction = ClosingAction.CloseWindow;
        }

        public static void ShowPopup(string Title, string Message)
        {
            var messagebox = new CustomMessageBox();

            messagebox.TopBar.TitleText_Text = Title;
            messagebox.textBlock.Text = Message;

            messagebox.Show();
        }
    }
}
