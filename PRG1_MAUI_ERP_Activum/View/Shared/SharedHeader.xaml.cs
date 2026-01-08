using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Shared;

public partial class SharedHeader : ContentView
{
    public bool IsLoggedIn => !string.IsNullOrEmpty(AppState.UserRole);

    public string UsernameText =>
        IsLoggedIn ? $"Inloggad som: {AppState.Username}" : "";

    public string RoleText =>
        AppState.UserRole switch
        {
            "Customer" => "Roll: Kund",
            "Employee" => "Roll: Anställd",
            _ => ""
        };

    public SharedHeader()
    {
        InitializeComponent();
        BindingContext = this;

        AppState.StateChanged += OnStateChanged;
        UpdateHeader();
    }

    private void OnStateChanged()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(UsernameText));
            OnPropertyChanged(nameof(RoleText));

            UpdateHeader();
        });
    }

    private void UpdateHeader()
    {
        switch (AppState.UserRole)
        {
            case "Customer":
                HeaderRoot.BackgroundColor = Color.FromArgb("#134f5c");
                TitleLabel.Text = "ACTIVUM";
                SubtitleLabel.Text = "Mina försäkringar";
                SubtitleLabel.IsVisible = true;
                break;

            case "Employee":
                HeaderRoot.BackgroundColor = Color.FromArgb("#666666");
                TitleLabel.Text = "ACTIVUM – ADMIN";
                SubtitleLabel.Text = "Administrationspanel";
                SubtitleLabel.IsVisible = true;
                break;

            default:
                HeaderRoot.BackgroundColor = Color.FromArgb("#134f5c");
                TitleLabel.Text = "ACTIVUM";
                SubtitleLabel.IsVisible = false;
                break;
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        AppState.Username = null;
        AppState.UserRole = null;
        AppState.NotifyStateChanged();

        await Shell.Current.GoToAsync("//StartPage");
    }
}
