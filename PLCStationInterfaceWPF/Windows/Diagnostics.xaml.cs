using PLCStationInterfaceWPF.Classes;
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
using WPFUtilsLib.Helpers;
using WPFUtilsLib.PLCs.Siemens;
using WPFUtilsLib.TCPIP;

namespace PLCStationInterfaceWPF.Windows
{
    public partial class Diagnostics : Page
    {
        private MySQLDatabase _mySQLDatabase;
        private Server _server;
        private SiemensPLC_1 _plc;

        public Diagnostics(SiemensPLC_1 plc, Server server, MySQLDatabase mySQLDatabase)
        {
            InitializeComponent();
            _plc = plc;
            _server = server;
            _mySQLDatabase = mySQLDatabase;

            StatusDotDatabase.Client = _mySQLDatabase;
            StatusDotPLC.Client = _plc;
            StatusDotTCPIPServer.Server = server;

            _plc.LiveUIntChanged += LiveUInt_Changed;
            _plc.ReadStatusCodeChanged += ReadStatusCode_Changed;
            _plc.WriteStatusCodeChanged += WriteStatusCode_Changed;
            _plc.LiveUIntStatusCodeChanged += LiveUIntStatus_Changed;
        }

        private void LiveUIntStatus_Changed(object sender, int e)
        {
            tblLiveUIntStatus.InvokeIfRequired((p) => p.Text = e.ToString());
            tbLiveUIntMessageStatus.InvokeIfRequired(p => p.Text = _plc.GetErrorMessage(e));
        }

        private void WriteStatusCode_Changed(object sender, int e)
        {
            tblWriteStatus.InvokeIfRequired((p) => p.Text = e.ToString());
            tbWriteStatusMessage.InvokeIfRequired(p => p.Text = _plc.GetErrorMessage(e));
        }

        private void ReadStatusCode_Changed(object sender, int e)
        {
            tblReadStatus.InvokeIfRequired((p) => p.Text = e.ToString());
            tbReadStatusMessage.InvokeIfRequired(p => p.Text = _plc.GetErrorMessage(e));
        }

        private void LiveUInt_Changed(object sender, ushort e)
        {
            tblLiveUInt.InvokeIfRequired((p) => p.Text = e.ToString());
        }
    }
}
