using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        UpdateMenuVisibility();
        AppState.StateChanged += UpdateFlyout;
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
        UpdateMenuVisibility();
    }

    private void UpdateMenuVisibility()
    {
        bool isLoggedIn = AppState.UserRole != null;

        CustomerMenu.IsVisible = AppState.UserRole == "Customer";
        EmployeeMenu.IsVisible = AppState.UserRole == "Employee";
        LogoutMenuItem.IsVisible = isLoggedIn;

        FlyoutBehavior = isLoggedIn
            ? FlyoutBehavior.Flyout
            : FlyoutBehavior.Disabled;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        AppState.Username = null;
        AppState.UserRole = null;
        AppState.NotifyStateChanged();

        await Shell.Current.GoToAsync("//StartPage");
    }
    private void UpdateFlyout()
    {
        var role = AppState.UserRole;

        FlyoutBehavior = string.IsNullOrEmpty(role)
            ? FlyoutBehavior.Disabled
            : FlyoutBehavior.Flyout;

        CustomerMenu.IsVisible = role == "Customer";
        EmployeeMenu.IsVisible = role == "Employee";
    }

}
