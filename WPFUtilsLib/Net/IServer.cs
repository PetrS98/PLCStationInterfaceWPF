
using System.Threading.Tasks;

namespace WPFUtilsLib.Net
{
    public interface IServer : IHasServerStatus
    {
        void Start();
        Task StartAsync();
        void Stop();
    }
}
