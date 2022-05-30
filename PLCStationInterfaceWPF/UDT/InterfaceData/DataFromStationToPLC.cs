using System;
using System.Collections.Generic;
using System.Text;

namespace PLCStationInterfaceWPF.UDT.InterfaceData
{
    public class DataFromStationToPLC
    {
        public int NonOperationID { get; set; } = 0;
        public bool StopAndResetCounting { get; set; } = false;
    }
}
