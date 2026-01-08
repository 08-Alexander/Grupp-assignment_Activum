using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Shared;

public partial class LogoutPage : ContentPage
{
    public LogoutPage()
    {
        InitializeComponent();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        AppState.Username = null;
        AppState.UserRole = null;
        AppState.NotifyStateChanged();

        await Shell.Current.GoToAsync("//StartPage");
    }
}
