using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;

namespace Dinory;

public partial class MainPage : ContentPage
{
    private readonly IAudioManager audioManager;

    public MainPage(IAudioManager audioManager)
    {
        InitializeComponent();
        this.audioManager = audioManager;
    }

    
    

    private async void OnClickStartGame(object sender, EventArgs e)
    {
        var player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("die_for_you.mp3"));
        //layer.Play();
  
        await Navigation.PushAsync(new DifficultyPage());
    }
    private void OnClickCloseGame(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private void ButtonClicked(object sender, EventArgs e)
    {
        this.ShowPopupAsync(new SettingsPage());
    }
}
