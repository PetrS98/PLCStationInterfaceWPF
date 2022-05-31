using System;
using System.Collections.Generic;
using System.Text;

namespace PLCStationInterfaceWPF.UDT.InterfaceData
{
    public class DataFromStationToPLC
    {
        public ushort StationID { get; set; } = 0;
        public int NonOperationID { get; set; } = 12;
        public bool StopAndResetCounting { get; set; } = true;
    }
}
