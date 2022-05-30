using Newtonsoft.Json;
using PLCStationInterfaceWPF.UDT.InterfaceData;
using System;
using System.Collections.Generic;
using System.Text;
using WPFUtilsLib.Helpers;
using WPFUtilsLib.Net;
using WPFUtilsLib.PLCs.Siemens;
using WPFUtilsLib.TCPIP;

namespace PLCStationInterfaceWPF.Classes
{
    public class PLCTCPServerDataHandler
    {
        const int STATION_COUNT = 4;

        private InterfaceDataUDT Data;
        public event EventHandler<InterfaceDataUDT> InterfaceDataChanged;

        Server _server;
        SiemensPLC_1 _plc;

        public PLCTCPServerDataHandler(Server server, SiemensPLC_1 plc)
        {
            _server = server;
            _plc = plc;
            Data = new InterfaceDataUDT();
            _plc.DataBufferReceived += DataFromPLC_Changed;

            
        }

        private void DataFromPLC_Changed(object sender, byte[] e)
        {
            if (Data is null) return;

            ReadDataFromPLC(e);
            WriteDataToStations(Data);

            InterfaceDataChanged?.Invoke(this, Data);
        }

        private void ReadDataFromPLC(byte[] plcData)
        {
            byte[,] StatusWord = new byte[STATION_COUNT, 2];

            for (int i = 0; i < STATION_COUNT; i++)
            {

                StatusWord[i, 0] = plcData[i * 2];
                StatusWord[i, 1] = plcData[(i * 2) + 1];

                Data.DataFromPLCToStation[i].StationCounterEnable = GetBit(StatusWord[0, 1], 0);
                Data.DataFromPLCToStation[i].StationCounterCounting = GetBit(StatusWord[0, 1], 1);
                Data.DataFromPLCToStation[i].StartCountingCommand = GetBit(StatusWord[0, 1], 2);
                Data.DataFromPLCToStation[i].StopCountingCommand = GetBit(StatusWord[0, 1], 3);
                Data.DataFromPLCToStation[i].ResetCountingCommand = GetBit(StatusWord[0, 1], 4);
                Data.DataFromPLCToStation[i].StationResetToMainApp = GetBit(StatusWord[0, 1], 5);
                Data.DataFromPLCToStation[i].ExternalStationStopAndReset = GetBit(StatusWord[0, 1], 6);
                Data.DataFromPLCToStation[i].StationError = GetBit(StatusWord[0, 1], 7);
            }
        }

        private void WriteDataToStations(InterfaceDataUDT Data)
        {
            if (_server.Status != ServerStatus.Running) return;

#pragma warning disable CS4014
            _server.BroadcastAsync(BuildJSONString(Data));
#pragma warning restore CS4014
        }

        private bool GetBit(byte dataByte, int bitPosition)
        {
            byte mask = 1;
            mask <<= bitPosition;

            return (dataByte & mask) > 0;
        }

        private string BuildJSONString(InterfaceDataUDT Data)
        {
            return JsonConvert.SerializeObject(Data, Formatting.Indented);
        }
    }
}
