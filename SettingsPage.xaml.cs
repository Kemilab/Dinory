using CommunityToolkit.Maui.Views;
using Dinory.Services;
using System;
using Microsoft.Maui.Controls;

namespace Dinory
{
    public partial class SettingsPage : Popup
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSwitchState();
        }

        private void LoadSwitchState()
        {
            AudioSwitch.IsToggled = Preferences.Get("AudioSwitchState", false);
            AudioPlayerService.Instance.ToggleAudio(AudioSwitch.IsToggled);
        }

        private void OnClickExit(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }

        public event EventHandler<bool> AudioToggled;

        private void OnAudioSwitchToggled(object sender, ToggledEventArgs e)
        {
            AudioToggled?.Invoke(sender, e.Value);
            AudioPlayerService.Instance.ToggleAudio(e.Value);
            Preferences.Set("AudioSwitchState", e.Value);
        }
    }
}
