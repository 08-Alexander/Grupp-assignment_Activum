namespace PRG1_MAUI_ERP_Activum.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime RegisteredDate { get; set; } = DateTime.Now;

    // Visningshjälp
    public string DisplayName => $"{Name} ({PersonalNumber})";
    public string Location => string.IsNullOrWhiteSpace(City) ? Address : $"{Address}, {City}";
}
