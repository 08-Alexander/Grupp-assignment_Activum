namespace PRG1_MAUI_ERP_Activum.Services;

public static class AuthService
{
    // username -> (password, role)
    private static readonly Dictionary<string, (string Password, string Role)> Users
        = new()
        {
            { "kund", ("1234", "Customer") },
            { "admin", ("9999", "Employee") },
            { "alexander", ("alexander", "Employee") },
            { "Simon", ("simkiv", "Employee") }

        };

    public static bool TryLogin(
        string username,
        string password,
        out string role)
    {
        role = null;

        if (!Users.TryGetValue(username, out var user))
            return false;

        if (user.Password != password)
            return false;

        role = user.Role;
        return true;
    }
}
