using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Shared;

public partial class SharedHeader : ContentView
{
    public SharedHeader()
    {
        InitializeComponent();
        UpdateUserInfo();

        AppState.StateChanged += UpdateUserInfo;
    }

    private void UpdateUserInfo()
    {
        if (AppState.UserRole == null)
        {
            UserInfoLabel.IsVisible = false;
        }
        else
        {
            UserInfoLabel.Text =
                $"Inloggad som: {AppState.Username} ({AppState.UserRole})";

            UserInfoLabel.IsVisible = true;
        }
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
                SubtitleLabel.Text = "Adminpanel";
                SubtitleLabel.IsVisible = true;
                break;

            default:
                HeaderRoot.BackgroundColor = Color.FromArgb("#134f5c");
                TitleLabel.Text = "ACTIVUM";
                SubtitleLabel.IsVisible = false;
                break;
        }
    }
}
