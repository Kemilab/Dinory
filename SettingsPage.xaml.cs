using CommunityToolkit.Maui.Views;

namespace Dinory;

public partial class SettingsPage : Popup
{



    public SettingsPage()
    {
        InitializeComponent();
    }
    private void OnClickExit(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {

    }
}



