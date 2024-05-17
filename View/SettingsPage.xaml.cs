using Service;

namespace WeatherWise.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void HapticFeedbackSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        // Save the new setting
        Preferences.Set("HapticFeedbackEnabled", e.Value);

        // Optionally perform a haptic feedback to demonstrate
        if (e.Value)
        {
            TryHapticFeedback();
        }
    }

    private void TryHapticFeedback()
    {
        if (Preferences.Get("HapticFeedbackEnabled", false))
        {
            // Assuming a method that handles haptic feedback:
            Vibration.Default.Vibrate();
        }
    }
    private void OnShowBatteryStatusClicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        ShowBatteryStatus();
    }

    private void ShowBatteryStatus()
    {
        var batteryLevel = Battery.ChargeLevel * 100;
        var batteryState = Battery.State.ToString();
        DisplayAlert("Battery Status", $"Battery Level: {batteryLevel:F0}% - Status: {batteryState}", "OK");
    }

    private void OnHowToUseClicked(object sender, EventArgs e)
    {
        HapticFeedbackHelper.PerformHapticFeedback();
        // Display an alert with instructions
        DisplayAlert("How to Use", "Use this page to turn on and off haptic feedback and check the battery status. " +
            " Switch right to enable haptic feedback. Click 'Show Battery Status' to view the current battery level and status.", "OK");
    }
}