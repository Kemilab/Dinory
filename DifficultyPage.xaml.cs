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
        await OnClickButtonBig(button, 100, 200);
        await Navigation.PopAsync();
    }
    
	private async void OnClickDifficultyEasy(object sender, EventArgs e)
	{
		Button button = (Button)sender;
        await OnClickButtonSmall(button, 100, 200);
		//samo za primjer
        await Navigation.PopAsync();
    }

}