using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics;
using System.Drawing;

namespace Dinory;

public partial class SettingsPage : Popup
{
	public SettingsPage()
	{
		InitializeComponent();
	}
	private void OnClickExit(object sender, EventArgs e)
	{
		//primjer
        Application.Current.Quit();
    }
	private void OnChangeExit(object sender, EventArgs e)
	{
		Application.Current.Quit();
	}
}