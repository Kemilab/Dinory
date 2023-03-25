namespace Dinory;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();

	}
    private void OnClickStartGame(object sender, EventArgs e)
    {
         Navigation.PushAsync(new DifficultyPage());
    }
    private void OnClickCloseGame(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}

