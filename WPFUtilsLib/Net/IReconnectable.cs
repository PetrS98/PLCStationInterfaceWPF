
namespace WPFUtilsLib.Net
{
    public interface IReconnectable
    {
        bool ReconnectEnabled { get; set; }
        int ReconnectInterval { get; set; }
    }
}
