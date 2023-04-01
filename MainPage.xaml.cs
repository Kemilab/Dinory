using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Plugin.Maui.Audio;
using System.IO;


namespace Dinory;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnClickStartGame(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await Buttons.OnClickButtonSmall(button, 100, 150);
        await Navigation.PushAsync(new DifficultyPage());
    }

    private void OnClickCloseGame(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void OnImageSettings(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new SettingsPage());
    }
}