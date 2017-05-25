using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Remotely.Click_Server_for_Windows.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public static class Win32
    {

        #region Cursor Position

        [System.Security.SuppressUnmanagedCodeSecurity]
        public static class Cursor
        {
            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

            [DllImport("user32.dll")]
            public static extern bool SetCursorPos(int X, int Y);

            [DllImport("user32.dll")]
            public static extern IntPtr GetCapture();

            [DllImport("user32.dll")]
            public static extern IntPtr SetCapture(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
        }

        #endregion
    }
}
