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
        await Navigation.PushAsync(new DifficultyPage());
    }
    private void OnClickCloseGame(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void Sound_Clicked(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new SettingsPage());
    }

    protected override void OnSizeAllocated(double pageWidth, double pageHeight)
    {
        base.OnSizeAllocated(pageWidth, pageHeight);
        const double aspectRatio = 3200 / 1745; // Aspect ratio of the original image
        backgroundImage.WidthRequest = Math.Max(pageHeight * aspectRatio, pageWidth);
    }
}