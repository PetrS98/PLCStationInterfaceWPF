using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFUtilsLib.Helpers;
using WPFUtilsLib.Net;

namespace WPFUtilsLib.UserControls.IOs
{
    public partial class ClientStatusDot : UserControl
    {
        private static readonly ImageSource GreenDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/GreenDot.png"));
        private static readonly ImageSource RedDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/RedDot.png"));
        private static readonly ImageSource WhiteDot = new BitmapImage(new Uri("pack://application:,,,/WPFUtilsLib;component/Resources/WhiteDot.png"));

        private System.Timers.Timer _refreshTimer = new System.Timers.Timer();

        private IHasClientStatus _client = null;
        private ClientStatus _status = ClientStatus.Disconnected;

        public IHasClientStatus Client
        {
            get => _client;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException();
                }

                if (!(_client is null))
                {
                    _client.StatusChanged -= (sender, e) => Status = e;
                }
                
                _client = value;
                _client.StatusChanged += (sender, e) => Status = e;
                Status = _client.Status;
            }
        }

        public ClientStatus Status
        {
            get => _status;
            private set
            {
                _status = value;

                if (_status == ClientStatus.Connected)
                {
                    _refreshTimer.Start();
                    return;
                }
                _refreshTimer.Stop();
                UpdateImage();
            }
        }

        public ClientStatusDot()
        {
            InitializeComponent();
            _refreshTimer.Interval = 500;
            _refreshTimer.Elapsed += (sender, e) => UpdateImage();
            UpdateImage();
        }

        private void UpdateImage()
        {
            imgDot.InvokeIfRequired((c) =>
            {
                c.Source = _status switch
                {
                    ClientStatus.Disconnected => RedDot,
                    ClientStatus.Connecting => WhiteDot,
                    ClientStatus.Connected => imgDot.Source = imgDot.Source == WhiteDot ? GreenDot : WhiteDot,
                    _ => throw new ArgumentOutOfRangeException(nameof(_status), $"Unexpected connection status value: {_status}")
                };
            });
        }
    }
}