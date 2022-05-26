using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFUtilsLib.Net;

namespace WPFUtilsLib.UserControls.IOs
{
    public partial class ServerStatusDot : UserControl
    {
        private static readonly ImageSource GreenDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/GreenDot.png"));
        private static readonly ImageSource RedDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/RedDot.png"));
        private static readonly ImageSource WhiteDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/WhiteDot.png"));

        private static System.Timers.Timer _refreshTimer = new System.Timers.Timer();

        private IHasServerStatus _server = null;
        private ServerStatus _status = ServerStatus.Stopped;

        public IHasServerStatus Server
        {
            get => _server;
            set
            {
                _ = value ?? throw new ArgumentNullException();

                if (_server != null)
                {
                    _server.StatusChanged -= (sender, e) => Status = e;
                }

                _server = value;
                _server.StatusChanged += (sender, e) => Status = e;
                Status = _server.Status;
            }
        }

        public ServerStatus Status
        {
            get => _status;
            private set
            {
                bool changed = _status != value;
                if (!changed) return;
                _status = value;

                if (_status == ServerStatus.Running)
                {
                    _refreshTimer.Start();
                    return;
                }
                _refreshTimer.Stop();
                UpdateImage();
            }
        }

        public ServerStatusDot()
        {
            InitializeComponent();
            _refreshTimer.Interval = 500;
            _refreshTimer.Elapsed += (sender, e) => UpdateImage();
            UpdateImage();
        }

        private void UpdateImage()
        {
            Dispatcher.Invoke(() =>
            {
                imgDot.Source = _status switch
                {
                    ServerStatus.Stopped => RedDot,
                    ServerStatus.Starting => WhiteDot,
                    ServerStatus.Running => imgDot.Source = imgDot.Source == WhiteDot ? GreenDot : WhiteDot,
                    _ => throw new ArgumentOutOfRangeException(nameof(_status), $"Unexpected server status value: {_status}")
                };
            });
        }
    }
}
