using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    internal static class ScrollHelper
    {
        private static class NativeMethods
        {
            public const int WM_SCROLL = 276; // Horizontal scroll
            public const int WM_VSCROLL = 277; // Vertical scroll
            public const int SB_LINEUP = 0; // Scrolls one line up
            public const int SB_LINELEFT = 0;// Scrolls one cell left
            public const int SB_LINEDOWN = 1; // Scrolls one line down
            public const int SB_LINERIGHT = 1;// Scrolls one cell right
            public const int SB_PAGEUP = 2; // Scrolls one page up
            public const int SB_PAGELEFT = 2;// Scrolls one page left
            public const int SB_PAGEDOWN = 3; // Scrolls one page down
            public const int SB_PAGERIGTH = 3; // Scrolls one page right
            public const int SB_PAGETOP = 6; // Scrolls to the upper left
            public const int SB_LEFT = 6; // Scrolls to the left
            public const int SB_PAGEBOTTOM = 7; // Scrolls to the upper right
            public const int SB_RIGHT = 7; // Scrolls to the right
            public const int SB_ENDSCROLL = 8; // Ends scroll

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        }

        public static void ScrollToLeft(Control ctrl)
        {
            NativeMethods.SendMessage(ctrl.Handle, NativeMethods.WM_SCROLL, (IntPtr)NativeMethods.SB_LEFT, IntPtr.Zero);
        }

        public static void ScrollToBottom(Control ctrl)
        {
            NativeMethods.SendMessage(ctrl.Handle, NativeMethods.WM_VSCROLL, (IntPtr)NativeMethods.SB_PAGEBOTTOM, IntPtr.Zero);
        }

        public static void ScrollToBottomLeft(Control ctrl)
        {
            ScrollToBottom(ctrl);
            ScrollToLeft(ctrl);
        }
    }
}
