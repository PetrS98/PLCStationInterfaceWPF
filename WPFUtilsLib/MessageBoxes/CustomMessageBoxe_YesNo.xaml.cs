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
    /// Interakční logika pro CustomMessageBoxe_YesNo.xaml
    /// </summary>
    public partial class CustomMessageBoxe_YesNo : Window
    {
        public CustomMessageBoxe_YesNo()
        {
            InitializeComponent();
            TopBar.Window = this;
            TopBar.ClosingAction = ClosingAction.CloseWindow;
        }

        public static bool ShowPopup(string Title, string Message)
        {
            var messagebox = new CustomMessageBox();

            messagebox.TopBar.TitleText_Text = Title;
            messagebox.textBlock.Text = Message;

            return (bool)messagebox.ShowDialog();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
