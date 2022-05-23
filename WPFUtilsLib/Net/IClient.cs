
using System.Threading.Tasks;

namespace WPFUtilsLib.Net
{
    public interface IClient : IHasClientStatus
    {
        void Connect();
        Task ConnectAsync();
        void Disconnect();
    }
}
