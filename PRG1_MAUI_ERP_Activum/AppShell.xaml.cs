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

        // Kund
        CustomerMenu.IsVisible = role == "Customer" || role == "Employee";

        // Endast admin
        InsuranceMenu.IsVisible = role == "Employee";
        ToolsMenu.IsVisible = role == "Employee";
        AboutMenu.IsVisible = isLoggedIn;
        HelpMenu.IsVisible = isLoggedIn;

        LogoutFlyout.IsVisible = isLoggedIn;
    }
}
