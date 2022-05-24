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
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.UserControls.IOs
{ 
    public partial class TopFormBar : UserControl
    {
        #region Icon Properties

            public ImageSource TitleImage_ImageSource {set { TitleIcon.Source = value; } }

        #endregion

        #region Title Text Properties

            public string TitleText_Text { get { return lblTitle.Content.ToString(); } set { lblTitle.Content = value; } }
            public Brush TitleText_BackColor { get { return lblTitle.Background; } set { lblTitle.Background = value; } }
            public Brush TitleText_ForeColor { get { return lblTitle.Foreground; } set { lblTitle.Foreground = value; } }
            public FontStyle TitleText_FontStyle { get { return lblTitle.FontStyle; } set { lblTitle.FontStyle = value; } }
            public double TitleText_FontSize { get { return lblTitle.FontSize; } set { lblTitle.FontSize = value; } }
            public FontWeight TitleText_FontWeight { get { return lblTitle.FontWeight; } set { lblTitle.FontWeight = value; } }
            public FontFamily TitleText_FontFamily { get { return lblTitle.FontFamily; } set { lblTitle.FontFamily = value; } }

        #endregion

        #region Close Button Properties

            public string CloseButton_Text { get { return btnClose.Content.ToString(); } set { btnClose.Content = value; } }
            public Brush CloseButton_BackColor { get { return btnClose.Background; } set { btnClose.Background = value; } }
            public Brush CloseButton_ForeColor { get { return btnClose.Foreground; } set { btnClose.Foreground = value; } }
            public FontStyle CloseButton_FontStile { get { return btnClose.FontStyle; } set { btnClose.FontStyle = value; } }
            public double CloseButton_FontSize { get { return btnClose.FontSize; } set { btnClose.FontSize = value; } }
            public FontWeight CloseButton_FontWeight { get { return btnClose.FontWeight; } set { btnClose.FontWeight = value; } }
            public FontFamily CloseButton_FontFamily { get { return btnClose.FontFamily; } set { btnClose.FontFamily = value; } }

        #endregion

        #region Border Properties

            public Brush Border_ThicknesBrush { get { return Border.BorderBrush; } set { Border.BorderBrush = value; } }
            public Thickness Border_BorderThickness { get { return Border.BorderThickness; } set { Border.BorderThickness = value; } }

        #endregion

        public Window Window { private get; set; }
        public ClosingAction ClosingAction { get; set; } = ClosingAction.CloseApp;

        public TopFormBar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (Window is null) return;

            if(ClosingAction == ClosingAction.CloseApp)
            {
                Application.Current.Shutdown();
            }
          
            Window.Close();
        }

        private void MoveableGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && Window != null)
            {
                Window.DragMove();
            }
        }
    }
}
