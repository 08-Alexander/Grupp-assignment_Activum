using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        AppState.StateChanged += UpdateFlyout;
        UpdateFlyout();
    }

    private void UpdateFlyout()
    {
        var role = AppState.UserRole;
        bool isLoggedIn = !string.IsNullOrEmpty(role);

        FlyoutBehavior = isLoggedIn
            ? FlyoutBehavior.Flyout
            : FlyoutBehavior.Disabled;

        CustomerMenu.IsVisible = role == "Customer";
        EmployeeMenu.IsVisible = role == "Employee";
        LogoutFlyout.IsVisible = isLoggedIn;
    }


    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        AppState.Username = null;
        AppState.UserRole = null;
        AppState.NotifyStateChanged();

        await Shell.Current.GoToAsync("//StartPage");
    }
}
