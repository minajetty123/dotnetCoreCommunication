using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace appCommunication
{
    class NotepadReader : INotepadReader
    {
        private const int WM_GETTEXT = 0xd;
        private const int WM_GETTEXTLENGTH = 0xe;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);

        private string GetText(IntPtr hWnd)
        {
            int textLength = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0) + 1;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(textLength);
            if (textLength > 0)
            {
                SendMessage(hWnd, WM_GETTEXT, textLength, sb);
            }
            return sb.ToString();
        }

        public string ReadContentFromNotepad()
        {
            Process[] ps = Process.GetProcessesByName("notepad");
            //we assume only one notepad open so far
            System.Diagnostics.Process p = ps[0];
            if (p == null)
            {
                throw new Exception("You need to open one notepad at least");
            }
            IntPtr editWnd = FindWindowEx(p.MainWindowHandle, IntPtr.Zero, "Edit", "");
            string content = GetText(editWnd);
            return content;
        }
    }
}
