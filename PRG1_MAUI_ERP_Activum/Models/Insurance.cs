namespace PRG1_MAUI_ERP_Activum.Models;

public class Insurance
{
    public int Id { get; set; }
    public string InsuranceNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;        // "Bil", "Hem", "Liv" …
    public decimal MonthlyCost { get; set; }
    public int CustomerId { get; set; }
    public string RiskLevel { get; set; } = string.Empty;  // "Låg" / "Medel" / "Hög"
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Today;
    public bool IsActive { get; set; } = true;

    // Visningshjälp
    public string StatusText => IsActive ? "Aktiv" : "Inaktiv";
    public string CostDisplay => $"{MonthlyCost:N0} kr/mån";
}
