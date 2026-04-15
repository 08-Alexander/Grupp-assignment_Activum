using System.Windows.Input;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.ViewModels;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(async () => await OnLogin());
    }

    private async Task OnLogin()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password))
            {
                await ErrorService.ShowError(
                    "Användarnamn och lösenord krävs");
                return;
            }

            if (!AuthService.TryLogin(Username, Password, out var role))
            {
                await ErrorService.ShowError(
                    "Felaktigt användarnamn eller lösenord");
                return;
            }

            AppState.Username = Username;
            AppState.UserRole = role;
            AppState.NotifyStateChanged();

            await Shell.Current.GoToAsync(
                role == "Customer"
                    ? "//CustomerHome"
                    : "//EmployeeHome");
        }
        catch (Exception ex)
        {
            await ErrorService.ShowUnexpected(ex);
        }
    }
}
