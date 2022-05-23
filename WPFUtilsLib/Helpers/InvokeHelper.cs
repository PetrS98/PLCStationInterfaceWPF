using System;
using System.Threading;
using System.Windows.Threading;

namespace WPFUtilsLib.Helpers
{
    public static class InvokeHelper
    {
        public static T InvokeIfRequired<T>(this T control, Action<T> action) where T : DispatcherObject
        {
            if (!control.Dispatcher.CheckAccess())
            {
                control.Dispatcher.Invoke(() => action(control));
            }
            else
            {
                action(control);
            }

            return control;
        }
    }
}
