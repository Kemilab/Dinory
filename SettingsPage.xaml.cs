using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;

namespace Dinory;

public partial class SettingsPage : Popup
{
    public SettingsPage()
    {
        InitializeComponent();
    }
    private void OnClickExit(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        bool isSwitchToggled = e.Value;
        if (isSwitchToggled == false)
        {
            var Player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("caves.mp3"));
            Player.Pause();
        }
        if (isSwitchToggled == true)
        {
            var Player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("caves.mp3"));
            Player.Play();
        }
    }
}




