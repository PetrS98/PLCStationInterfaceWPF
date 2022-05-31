namespace PLCStationInterfaceWPF.UDT.InterfaceData
{
    public class DataFromPLCToStation
    {
        public ushort StationID { get; set; } = 0;
        public bool StationCounterEnable { get; set; } = false;
        public bool StationCounterCounting { get; set; } = false;
        public bool StartCountingCommand { get; set; } = false;
        public bool StopCountingCommand { get; set; } = false;
        public bool ResetCountingCommand { get; set; } = false;
        public bool StationResetToMainApp { get; set; } = false;
        public bool ExternalStationStopAndReset { get; set; } = false;
        public bool StationError { get; set; }

        public bool Spare_1 { get; set; } = false;
        public bool Spare_2 { get; set; } = false;
        public bool Spare_3 { get; set; } = false;
        public bool Spare_4 { get; set; } = false;
        public bool Spare_5 { get; set; } = false;
        public bool Spare_6 { get; set; } = false;
        public bool Spare_7 { get; set; } = false;
        public bool Spare_8 { get; set; } = false;
    }
}
