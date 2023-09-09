using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPL_Gateway
{
     class CustomKey
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public static void PressKey(Keys key, bool up)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            }
        }

        public static void CtrlPlusKey(Keys key)
        {
            PressKey(Keys.ControlKey, false);
            PressKey(key, false);
            PressKey(key, true);
            PressKey(Keys.ControlKey, true);
        }


        public static void CtrlAltPlusKey(Keys key)
        {
            PressKey(Keys.ControlKey, false);
            PressKey(Keys.Alt, false);
            PressKey(key, false);
            PressKey(key, true);
            PressKey(Keys.Alt, true);
            PressKey(Keys.ControlKey, true);
        }
    }
}
