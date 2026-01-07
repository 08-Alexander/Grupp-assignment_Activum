using System.Windows.Input;

namespace PRG1_MAUI_ERP_Activum.ViewModels;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLogin);
    }

    private async void OnLogin()
    {
        if (Username == "kund")
        {
            await Shell.Current.GoToAsync("//CustomerHome");
        }
        else if (Username == "admin")
        {
            await Shell.Current.GoToAsync("//EmployeeHome");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert(
                "Fel",
                "Felaktigt användarnamn eller lösenord",
                "OK");
        }
    }
}
