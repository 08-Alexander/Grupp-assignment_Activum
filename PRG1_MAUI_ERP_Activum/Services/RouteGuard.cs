namespace PRG1_MAUI_ERP_Activum.Services;

public static class RouteGuard
{
    public static bool IsRouteAllowed(string route)
    {
        var role = AppState.UserRole;

        // ? Ej inloggad
        if (string.IsNullOrEmpty(role))
            return route == "StartPage" || route == "LoginPage";

        // ?? Kund
        if (role == "Customer")
        {
            return route switch
            {
                "MainPage" => true,
                "StartPage" => true,
                _ => false
            };
        }

        // ????? Anställd
        if (role == "Employee")
        {
            return route switch
            {
                "CustomersPage" => true,
                "InsurancePage" => true,
                "ToolsPage" => true,
                "StartPage" => true,
                _ => false
            };
        }

        return false;
    }
}
