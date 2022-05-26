namespace PLCStationInterfaceWPF.JDO
{
    public class PLCSettingsJDO
    {
        public int UpdateInterval { get; set; } = 150;
        public string IPAddress { get; set; } = "192.168.1.5";
        public int Rack { get; set; } = 0;
        public int Slot { get; set; } = 1;

        public int ReadDBNumber { get; set; } = 10;
        public int ReadDataBufferOffset { get; set; } = 0;
        public int ReadDataBufferSize { get; set; } = 1;

        public int WriteDBNumber { get; set; } = 10;
        public int WriteDataBufferOffset { get; set; } = 0;
        public int WriteDataBufferSize { get; set; } = 1;

        public int LiveUIntDBNumber { get; set; } = 10;
        public int LiveUIntOffset { get; set; } = 0;
        public int LiveUIntBufferSize { get; set; } = 1;
    }
}
