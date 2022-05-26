using System.Windows;
using System.Windows.Media;
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.MessageBoxes
{
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
