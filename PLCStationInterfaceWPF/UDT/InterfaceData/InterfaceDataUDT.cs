namespace PLCStationInterfaceWPF.UDT.InterfaceData
{
    public class InterfaceDataUDT
    {
        public DataFromPLCToStation[] DataFromPLCToStation { get; set; } = new DataFromPLCToStation[4];
        public DataFromStationToPLC[] DataFromStationToPLC { get; set; } = new DataFromStationToPLC[4];

        public InterfaceDataUDT()
        {
            for (int i = 0; i < 4; i++)
            {
                DataFromPLCToStation[i] = new DataFromPLCToStation();
                DataFromStationToPLC[i] = new DataFromStationToPLC();
                DataFromPLCToStation[i].StationID = (ushort)(i + 1);
                DataFromStationToPLC[i].StationID = (ushort)(i + 1);
            }
        }
    }
}
