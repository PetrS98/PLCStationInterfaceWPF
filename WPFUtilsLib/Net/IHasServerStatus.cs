using System;

namespace WPFUtilsLib.Net
{
    public interface IHasServerStatus
    {
        event EventHandler<ServerStatus> StatusChanged;
        ServerStatus Status { get; }
    }
}
