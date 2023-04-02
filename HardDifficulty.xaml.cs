using CommunityToolkit.Maui.Views;

namespace Dinory;

public partial class HardDifficulty: ContentPage
{
	public HardDifficulty()
	{
		InitializeComponent();
	}
    private async void OnClickSettings(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new SettingsPage());
    }
}