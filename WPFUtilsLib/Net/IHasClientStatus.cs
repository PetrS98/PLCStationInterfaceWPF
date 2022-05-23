using System;

namespace WPFUtilsLib.Net
{
    public interface IHasClientStatus
    {
        event EventHandler<ClientStatus> StatusChanged;
        ClientStatus Status { get; }
    }
}
