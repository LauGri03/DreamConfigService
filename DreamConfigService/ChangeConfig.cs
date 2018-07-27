using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Drawing;




namespace DreamConfigService
{
    public static class ChangeConfig
    {
        [DllImport("gdi32.dll")]
        private unsafe static extern bool SetDeviceGammaRamp(Int32 hdc, void* ramp);
        private static bool initialized = false;
        private static Int32 hdc;
        private static int a;


        private static void changeVolume(int vol)
        {
            if (vol > 100)
                vol = 100;
            if (vol < 0)
                vol = 0;
            CoreAudioDevice device = new CoreAudioController().DefaultPlaybackDevice;
            device.Volume = vol;
            device.Dispose();     
       }

        private static void changeKeyboard(string key)
        {
            try
            {
                CultureInfo TypeOfLanguage = CultureInfo.CreateSpecificCulture('"' + key + '"');
                System.Threading.Thread.CurrentThread.CurrentCulture = TypeOfLanguage;
                InputLanguage l = InputLanguage.FromCulture(TypeOfLanguage);
                InputLanguage.CurrentInputLanguage = l;
            }
            catch
            {
                return;
            }
        }

        private static void InitiliazeClass()
        {
            if (initialized)
                return;
            hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
            initialized = true;
        }
        private static unsafe bool changeGamma(int gamma)
        {
            InitiliazeClass();
            if (gamma > 255)
                gamma = 255;
            if (gamma < 0)
                gamma = 0;
            short* gArray = stackalloc short[3 * 256];
            short* idx = gArray;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 256; i++)
                {
                    int arrayVal = i * (gamma + 128);
                    if (arrayVal > 65535)
                        arrayVal = 65535;
                    *idx = (short)arrayVal;
                    idx++;
                }
            }
            bool retVal = SetDeviceGammaRamp(hdc, gArray);
            return retVal;
        }


        public static void changeConfig(int vol, string keyboard,int gamma)
        {
            changeVolume(vol);
            changeKeyboard(keyboard);
            changeGamma(gamma);
        }

        public static void changeConfig(int vol,string keyboard)
        {
            changeVolume(vol);
            changeKeyboard(keyboard);
        }

    }
}
