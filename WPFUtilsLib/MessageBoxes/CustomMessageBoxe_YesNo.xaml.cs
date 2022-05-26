using System.Windows;
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.MessageBoxes
{
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
            var messagebox = new CustomMessageBoxe_YesNo();

            messagebox.TopBar.TitleText_Text = Title;
            messagebox.textBlock.Text = Message;

            return (bool)messagebox.ShowDialog();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
