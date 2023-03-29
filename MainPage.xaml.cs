using CommunityToolkit.Maui.Views;



namespace Dinory;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }
    public static async Task OnClickButtonSmall(Button Button, int scaleDuration, int delayDuration)
    {
        await Button.ScaleTo(0.9, (uint)scaleDuration);
        await Task.Delay(delayDuration);
        await Button.ScaleTo(1, (uint)scaleDuration);
        await Task.Delay(delayDuration);
        await Button.ScaleTo(1, (uint)scaleDuration);
    }
    private async void OnClickStartGame(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await OnClickButtonSmall(button, 100, 150);
        await Navigation.PushAsync(new DifficultyPage());
    }
    private void OnClickCloseGame(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }



}