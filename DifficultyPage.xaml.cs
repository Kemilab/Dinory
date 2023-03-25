namespace Dinory;

public partial class DifficultyPage : ContentPage
{
	public DifficultyPage()
	{
		InitializeComponent();
	}
    private async void Return(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
    
}