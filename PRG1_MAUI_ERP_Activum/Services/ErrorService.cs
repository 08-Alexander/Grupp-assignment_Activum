namespace PRG1_MAUI_ERP_Activum.Services;

public static class ErrorService
{
    public static async Task ShowError(
        string message,
        string title = "Fel")
    {
        if (Application.Current?.MainPage == null)
            return;

        await Application.Current.MainPage.DisplayAlert(
            title,
            message,
            "OK");
    }

    public static async Task ShowUnexpected(Exception ex)
    {
        await ShowError(
            "Något gick fel. Försök igen.",
            "Oväntat fel");

#if DEBUG
        System.Diagnostics.Debug.WriteLine(ex);
#endif
    }
}
