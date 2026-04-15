using PRG1_MAUI_ERP_Activum.Models;

namespace PRG1_MAUI_ERP_Activum.Services;

public static class InsuranceRegister
{
    public static List<Insurance> Insurances { get; } = new();

    public static void Add(Insurance insurance)
    {
        insurance.Id = Insurances.Count + 1;
        insurance.InsuranceNumber = $"INS-{insurance.Id:000}";
        Insurances.Add(insurance);
    }

    public static List<Insurance> GetByCustomer(int customerId)
    {
        return Insurances
            .Where(i => i.CustomerId == customerId)
            .ToList();
    }
}