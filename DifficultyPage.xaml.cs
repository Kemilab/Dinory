namespace Dinory;

public partial class DifficultyPage : ContentPage
{
	public DifficultyPage()
	{
		InitializeComponent();
	}
	private async void OnClickReturnPage(object sender, EventArgs e)
	{
		var button = sender as ImageButton;
		await button.ScaleTo(1.5, 100);
		await Task.Delay(200);
		await button.ScaleTo(1, 100);
        await Task.Delay(200);
        await Navigation.PopAsync();
	}
    public static async Task OnClickButtonAnimation(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await Task.Delay(200);
        await button.ScaleTo(1, 100);
        await Task.Delay(200);
    }
	private async void OnClickDifficultyEasy(object sender, EventArgs e)
	{
		await DifficultyPage.OnClickButtonAnimation(sender, e);
		//samo za primjer
        await Navigation.PopAsync();
    }

}