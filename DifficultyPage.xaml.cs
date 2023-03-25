namespace Dinory;

public partial class DifficultyPage : ContentPage
{
	public DifficultyPage()
	{
		InitializeComponent();
	}
    private void OnClickReturnPage(object sender, EventArgs e)
	{
		 Navigation.PopAsync();
	}
    
}