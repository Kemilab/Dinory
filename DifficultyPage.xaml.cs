using CommunityToolkit.Maui.Views;

namespace Dinory;

public partial class DifficultyPage : ContentPage
{
    public DifficultyPage()
    {
        InitializeComponent();
    }

    public static async Task OnClickButtonBig(ImageButton ImageButton, int scaleDuration, int delayDuration)
    {
        await ImageButton.ScaleTo(1.3, (uint)scaleDuration);
        await Task.Delay(delayDuration);
        await ImageButton.ScaleTo(1, (uint)scaleDuration);
        await Task.Delay(delayDuration);
    }

    public static async Task OnClickButtonSmall(Button Button, int scaleDuration, int delayDuration)
    {
        await Button.ScaleTo(0.9, (uint)scaleDuration);
        await Task.Delay(delayDuration);
        await Button.ScaleTo(1, (uint)scaleDuration);
        await Task.Delay(delayDuration);
    }

    private async void OnClickReturnPage(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        await OnClickButtonBig(button, 100, 150);
        await Navigation.PopAsync();
    }

    private async void OnClickDifficultyEasy(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await OnClickButtonSmall(button, 100, 150);
        //samo za primjer
        await Navigation.PushAsync(new EasyDifficulty());
    }

    private async void OnClickSettings(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        await OnClickButtonBig(button, 100, 100);
        await this.ShowPopupAsync(new SettingsPage());
    }
}