using PRG1_MAUI_ERP_Activum.Services;


namespace PRG1_MAUI_ERP_Activum.Services;

public static class AppState
{
    public static string? UserRole { get; set; }   // "Customer" | "Employee"
    public static string? Username { get; set; }

    public static event Action? StateChanged;

    public static void NotifyStateChanged()
    {
        StateChanged?.Invoke();
    }
}

