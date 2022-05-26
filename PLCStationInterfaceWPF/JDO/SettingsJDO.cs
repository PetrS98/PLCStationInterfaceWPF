using Newtonsoft.Json;
using PLCStationInterfaceWPF.JDO;
using PLCStationInterfaceWPF.JDO.SettingsLogin;

namespace PLCStationInterfaceWPF.UDT
{
    public class SettingsJDO
    {
        public PLCSettingsJDO PLCSettings { get; set; } = new PLCSettingsJDO();
        public TCPServerSettingsJDO TCPServerSettings { get; set; } = new TCPServerSettingsJDO();
        public SettingsLoginJDO SettingsLogin { get; set; } = new SettingsLoginJDO();
        public DatabaseSettingsJDO DatabaseSettings { get; set; } = new DatabaseSettingsJDO();

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static SettingsJDO Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SettingsJDO>(json);
        }
    }
}
