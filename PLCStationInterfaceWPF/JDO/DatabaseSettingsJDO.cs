namespace PLCStationInterfaceWPF.JDO
{
    public class DatabaseSettingsJDO
    {
        public string IPAddress { get; set; } = "192.168.1.1";
        public string DatabaseName { get; set; } = "Database";
        public string NonOPMessageTable { get; set; } = "Table";
        public string DatabaseUserName { get; set; } = "User";
        public string DatabasePassword { get; set; } = "1234";
    }
}
