using PLCStationInterfaceWPF.Classes;
using PLCStationInterfaceWPF.JDO;
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
using WPFUtilsLib.MessageBoxes;
using WPFUtilsLib.Net;
using WPFUtilsLib.PLCs.Siemens;
using WPFUtilsLib.UserControls.IOs;

namespace PLCStationInterfaceWPF.Windows.Settings
{
    public partial class PLCSettings : Page
    {
        private PLCSettingsJDO _PLCSettings;
        private SiemensPLC_1 _plc;

        private string MessageMessageBoxTitle = "";
        private string Message = "";

        public PLCSettings(PLCSettingsJDO plcSettings, SiemensPLC_1 plc)
        {
            InitializeComponent();

            Translator.LanguageChanged += Translate;

            _PLCSettings = plcSettings;
            _plc = plc;

            SetValuesFromJasonToControls(_PLCSettings);

            _plc.StatusChanged += Status_Changed;
        }

        private void Translate(object sender, EnumLanguage e)
        {

            if (Translator.Language == EnumLanguage.CZ)
            {
                #region Controls Texts

                #region Main

                lblTitle.Text = "Nastavení PLC";

                #endregion

                #region General Settings

                lblGeneralSettingsTitle.Text = "Obecné Nastavení";
                lblIPAddress.Text = "IP Adresa:";
                lblUpdateInterval.Text = "Interval Aktualizace:";
                lblOLCRack.Text = "PLC Rack";
                lblPLCSlot.Text = "PLC Slot";

                #endregion

                #region LiveUInt Settings

                lblLiveUIntSettingsTitle.Text = "Nastavení Live UIntu";
                lblLiveUIntDB.Text = "DB Live UIntu:";
                lblLiveUIntOffset.Text = "Offset Live UIntu:";
                lblLiveUIntSize.Text = "Délka Live UIntu:";

                #endregion

                #region Read / Write Settings

                lblReadWriteSettingsTitle.Text = "Nastavení Čtení / Zápisu";
                lblReadDB.Text = "DB pro Čtení:";
                lblReadDataOffset.Text = "Offset Dat pro Čtení";
                lblReadDataSize.Text = "Délka Dat pro Čtení:";

                lblWriteDB.Text = "DB pro Zápis:";
                lblWriteDataOffset.Text = "Offset Dat pro Zápis:";
                lblWriteDataSize.Text = "Délka Dat pro Zápis:";

                #endregion

                #region Buttons

                btnConnect.Content = "Připojit";
                btnDisconnect.Content = "Odpojit";
                btnApply.Content = "Použít";

                #endregion

                #endregion

                #region Messages Texts

                MessageMessageBoxTitle = "Informace";

                Message = "Data byla úspěšně uložena.";

                #endregion
            }
            else if (Translator.Language == EnumLanguage.ENG)
            {
                #region Controls Texts

                #region Main

                lblTitle.Text = "PLC Settings";

                #endregion

                #region General Settings

                lblGeneralSettingsTitle.Text = "General Settings:";
                lblIPAddress.Text = "IP Address:";
                lblUpdateInterval.Text = "Update Interval:";
                lblOLCRack.Text = "PLC Rack";
                lblPLCSlot.Text = "PLC Slot";

                #endregion

                #region LiveUInt Settings

                lblLiveUIntSettingsTitle.Text = "Live UInt Settings";
                lblLiveUIntDB.Text = "Live UInt DB:";
                lblLiveUIntOffset.Text = "Live UInt Offset:";
                lblLiveUIntSize.Text = "Live UInt Size:";

                #endregion

                #region Read / Write Settings

                lblReadWriteSettingsTitle.Text = "Read / Write Settings";
                lblReadDB.Text = "Read DB:";
                lblReadDataOffset.Text = "Read Data Offset:";
                lblReadDataSize.Text = "Read Data Size:";

                lblWriteDB.Text = "Write DB:";
                lblWriteDataOffset.Text = "Write Data Offset:";
                lblWriteDataSize.Text = "Write Data Size:";

                #endregion

                #region Buttons

                btnConnect.Content = "Connect";
                btnDisconnect.Content = "Disconnect";
                btnApply.Content = "Apply";

                #endregion

                #endregion

                #region Messages Texts

                MessageMessageBoxTitle = "Information";

                Message = "Data was correctly saved.";

                #endregion
            }
        }

        private void Status_Changed(object sender, ClientStatus e)
        {
            if (e.Equals(ClientStatus.Connected))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
                //btnConnect.InvokeIfRequired(btn => btn.Cursor = Cursors.No);

                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
                //btnDisconnect.InvokeIfRequired(btn => btn.Cursor = default);
            }
            else if (e.Equals(ClientStatus.Disconnected))
            {
                btnConnect.InvokeIfRequired((btn) => btn.IsEnabled = true);
                //btnConnect.InvokeIfRequired(btn => btn.Cursor = default);

                btnDisconnect.InvokeIfRequired((btn) => btn.IsEnabled = false);
                //btnDisconnect.InvokeIfRequired(btn => btn.Cursor = Cursors.No);
            }
        }

        private void SetValuesFromControlsToJason(ref PLCSettingsJDO Settings)
        {
            Settings.IPAddress = ipab.IPAddress;
            Settings.UpdateInterval = tbUpdateInterval.Value;
            Settings.Rack = tbPLCRack.Value;
            Settings.Slot = tbPLCSlot.Value;

            Settings.LiveUIntDBNumber = tbLiveUIntDB.Value;
            Settings.LiveUIntOffset = tbLiveUIntOffset.Value;
            Settings.LiveUIntBufferSize = tbLiveUIntSize.Value;

            Settings.ReadDBNumber = tbReadDB.Value;
            Settings.ReadDataBufferOffset = tbReadDataOffset.Value;
            Settings.ReadDataBufferSize = tbReadDataSize.Value;

            Settings.WriteDBNumber = tbWriteDB.Value;
            Settings.WriteDataBufferOffset = tbWriteDataOffset.Value;
            Settings.WriteDataBufferSize = tbWriteDataSize.Value;
        }

        private void SetValuesFromJasonToControls(PLCSettingsJDO Settings)
        {
            ipab.IPAddress = Settings.IPAddress;
            tbUpdateInterval.Value = Settings.UpdateInterval;
            tbPLCRack.Value = Settings.Rack;
            tbPLCSlot.Value = Settings.Slot;

            tbLiveUIntDB.Value = Settings.LiveUIntDBNumber;
            tbLiveUIntOffset.Value = Settings.LiveUIntOffset;
            tbLiveUIntSize.Value = Settings.LiveUIntBufferSize;

            tbReadDB.Value = Settings.ReadDBNumber;
            tbReadDataOffset.Value = Settings.ReadDataBufferOffset;
            tbReadDataSize.Value = Settings.ReadDataBufferSize;

            tbWriteDB.Value = Settings.WriteDBNumber;
            tbWriteDataOffset.Value = Settings.WriteDataBufferOffset;
            tbWriteDataSize.Value = Settings.WriteDataBufferSize;
        }

        private void SetParametrsToPLCAndConnect(PLCSettingsJDO Settings, ref SiemensPLC_1 PLC, bool Connect)
        {
            if (PLC is null) return;

            PLC.IPAddress = Settings.IPAddress;
            PLC.UpdateInterval = Settings.UpdateInterval;
            PLC.Rack = Settings.Rack;
            PLC.Slot = Settings.Slot;
            PLC.ReconnectInterval = 4000;
            PLC.ReconnectEnabled = true;

            PLC.LiveUIntDBNumber = Settings.LiveUIntDBNumber;
            PLC.LiveUIntOffset = Settings.LiveUIntOffset;
            PLC.LiveUIntBufferSize = Settings.LiveUIntBufferSize;

            PLC.ReadDBNumber = Settings.ReadDBNumber;
            PLC.ReadDataBufferOffset = Settings.ReadDataBufferOffset;
            PLC.ReadDataBufferSize = Settings.ReadDataBufferSize;

            PLC.WriteDBNumber = Settings.WriteDBNumber;
            PLC.WriteDataBufferOffset = Settings.WriteDataBufferOffset;
            PLC.WriteDataBufferSize = Settings.WriteDataBufferSize;

            if (PLC.Status != ClientStatus.Disconnected) return;
            if (Connect == false) return;

#pragma warning disable CS4014
            PLC.ConnectAsync();
#pragma warning restore CS4014
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            if(_plc.Status != ClientStatus.Disconnected) return;

            SetParametrsToPLCAndConnect(_PLCSettings, ref _plc, true);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            _plc.ReconnectEnabled = false;
            _plc.Disconnect();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.CheckLogin() == false) return;

            SetValuesFromControlsToJason(ref _PLCSettings);

            CustomMessageBox.ShowPopup(MessageMessageBoxTitle, Message);

            SetParametrsToPLCAndConnect(_PLCSettings, ref _plc, false);
        }

        private void pPLCSettings_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetValuesFromJasonToControls(_PLCSettings);
        }
    }
}
