using CommunityToolkit.Maui.Views;

namespace Dinory;

public partial class DifficultyPage : ContentPage
{
    public DifficultyPage()
    {
        InitializeComponent();
    }

    private async void OnClickReturnPage(object sender, EventArgs e)
    { 

        await Navigation.PopAsync();
    }
     private async void OnClickSettings(object sender, EventArgs e)
     {
        await this.ShowPopupAsync(new SettingsPage());
     }

    [Obsolete]
    private async void OnClickDifficultyEasy(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await Buttons.OnClickButtonSmall(button, 100, 150);
        await Navigation.PushAsync(new EasyDifficulty());
    }

    private async void OnClickDifficultyNormal(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await Buttons.OnClickButtonSmall(button, 100, 150);
        await Navigation.PushAsync(new NormalDifficulty());
    }

    private async void Hard_Clicked_1(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await Buttons.OnClickButtonSmall(button, 100, 150);
        await Navigation.PushAsync(new hard());
    }
}