using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PRG1_MAUI_ERP_Activum.Models;

namespace PRG1_MAUI_ERP_Activum.ViewModels;

/// <summary>
/// Delad ViewModel som lever under hela applikationens livslängd.
/// Håller ObservableCollection för Customer och Insurance så att
/// samma listor kan användas på flera sidor (CustomerRegisterPage,
/// InsuranceRegisterPage, MainPage osv.).
/// </summary>
public class InsuranceViewModel : INotifyPropertyChanged
{
    // ── Singleton ────────────────────────────────────────────────────────────
    private static InsuranceViewModel? _instance;
    public static InsuranceViewModel Instance => _instance ??= new InsuranceViewModel();

    // ── Listor ───────────────────────────────────────────────────────────────
    public ObservableCollection<Customer>  Customers  { get; } = new();
    public ObservableCollection<Insurance> Insurances { get; } = new();

    // ── ID-räknare ───────────────────────────────────────────────────────────
    private int _nextCustomerId  = 1;
    private int _nextInsuranceId = 1;

    // ── Konstruktor med exempeldata ──────────────────────────────────────────
    private InsuranceViewModel()
    {
        SeedData();
    }

    private void SeedData()
    {
        var c1 = AddCustomer(new Customer
        {
            Name           = "Anna Johansson",
            Email          = "anna@example.se",
            Phone          = "070-1234567",
            PersonalNumber = "19850312-1234",
            Address        = "Storgatan 1",
            City           = "Göteborg",
            RegisteredDate = new DateTime(2020, 1, 15)
        });

        var c2 = AddCustomer(new Customer
        {
            Name           = "Erik Lindqvist",
            Email          = "erik@example.se",
            Phone          = "073-9876543",
            PersonalNumber = "19781125-5678",
            Address        = "Kungsgatan 22",
            City           = "Göteborg",
            RegisteredDate = new DateTime(2019, 6, 3)
        });

        AddInsurance(new Insurance
        {
            Type        = "Hem",
            Description = "Hemförsäkring villa",
            RiskLevel   = "Låg",
            MonthlyCost = 249m,
            CustomerId  = c1.Id,
            StartDate   = new DateTime(2020, 2, 1),
            IsActive    = true
        });

        AddInsurance(new Insurance
        {
            Type        = "Bil",
            Description = "Helförsäkring personbil",
            RiskLevel   = "Medel",
            MonthlyCost = 595m,
            CustomerId  = c1.Id,
            StartDate   = new DateTime(2021, 5, 15),
            IsActive    = true
        });

        AddInsurance(new Insurance
        {
            Type        = "Liv",
            Description = "Livförsäkring 2 Mkr",
            RiskLevel   = "Låg",
            MonthlyCost = 189m,
            CustomerId  = c2.Id,
            StartDate   = new DateTime(2019, 7, 1),
            IsActive    = true
        });
    }

    // ── Kundmetoder ──────────────────────────────────────────────────────────

    public Customer AddCustomer(Customer customer)
    {
        customer.Id = _nextCustomerId++;
        Customers.Add(customer);
        return customer;
    }

    public void RemoveCustomer(Customer customer)
    {
        // Ta bort kundens försäkringar också (LINQ)
        var related = Insurances.Where(i => i.CustomerId == customer.Id).ToList();
        foreach (var ins in related)
            Insurances.Remove(ins);

        Customers.Remove(customer);
    }

    // ── Försäkringsmetoder ───────────────────────────────────────────────────

    public void AddInsurance(Insurance insurance)
    {
        insurance.Id = _nextInsuranceId++;
        insurance.InsuranceNumber = $"INS-{insurance.Id:000}";
        Insurances.Add(insurance);
    }

    public void RemoveInsurance(Insurance insurance) =>
        Insurances.Remove(insurance);

    // ── LINQ-hjälpmetoder ────────────────────────────────────────────────────

    /// <summary>Alla försäkringar för en specifik kund (LINQ).</summary>
    public IEnumerable<Insurance> GetByCustomer(int customerId) =>
        Insurances.Where(i => i.CustomerId == customerId);

    /// <summary>Aktiva försäkringar sorterade efter typ (LINQ).</summary>
    public IEnumerable<Insurance> GetActiveSorted() =>
        Insurances.Where(i => i.IsActive).OrderBy(i => i.Type);

    /// <summary>Sök kunder på namn eller personnummer (LINQ).</summary>
    public IEnumerable<Customer> SearchCustomers(string query) =>
        string.IsNullOrWhiteSpace(query)
            ? Customers.OrderBy(c => c.Name)
            : Customers
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            c.PersonalNumber.Contains(query))
                .OrderBy(c => c.Name);

    // ── INotifyPropertyChanged ───────────────────────────────────────────────
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
