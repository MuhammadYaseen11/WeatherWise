using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class HapticFeedbackHelper
    {
        public static void PerformHapticFeedback()
        {
            if (Preferences.Get("HapticFeedbackEnabled", false))
            {
                try
                {
                    Vibration.Default.Vibrate();
                }
                catch (Exception ex)
                {
                    // Handle if the device does not support vibration
                    Console.WriteLine("Haptic feedback failed: " + ex.Message);
                }
            }
        }
    }

}
