using CommunityToolkit.Maui.Views;
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

    private async void OnClickSettings(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        await Buttons.OnClickButtonBig(button, 100, 150);
        await this.ShowPopupAsync(new SettingsPage());
    }


}