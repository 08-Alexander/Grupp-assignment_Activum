using PRG1_MAUI_ERP_Activum.Models;
using PRG1_MAUI_ERP_Activum.ViewModels;

namespace PRG1_MAUI_ERP_Activum.View.Customers;

public partial class CustomerRegisterPage : ContentPage
{
    // Hämtar den delade ViewModeln (singleton)
    private readonly InsuranceViewModel _vm = InsuranceViewModel.Instance;

    public CustomerRegisterPage()
    {
        InitializeComponent();
        RefreshList();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

    // ── LINQ: sortera kunder efter namn ──────────────────────────────────────
    private void RefreshList()
    {
        CustomerList.ItemsSource = _vm.Customers
            .OrderBy(c => c.Name)
            .ToList();
    }

    // ── Lägg till kund ───────────────────────────────────────────────────────
    private async void OnAddCustomerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryName.Text))
        {
            await DisplayAlert("Obligatoriskt fält", "Namn måste fyllas i.", "OK");
            return;
        }

        var customer = new Customer
        {
            Name           = EntryName.Text.Trim(),
            Email          = EntryEmail.Text?.Trim()          ?? string.Empty,
            Phone          = EntryPhone.Text?.Trim()          ?? string.Empty,
            PersonalNumber = EntryPersonalNumber.Text?.Trim() ?? string.Empty,
            Address        = EntryAddress.Text?.Trim()        ?? string.Empty,
            City           = EntryCity.Text?.Trim()           ?? string.Empty
        };

        _vm.AddCustomer(customer);
        ClearForm();
        RefreshList();
    }

    // ── Ta bort kund ─────────────────────────────────────────────────────────
    private async void OnRemoveCustomerClicked(object sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: Customer customer })
        {
            bool confirm = await DisplayAlert(
                "Ta bort kund",
                $"Vill du ta bort {customer.Name}?\nAlla kundens försäkringar tas också bort.",
                "Ja", "Avbryt");

            if (confirm)
            {
                _vm.RemoveCustomer(customer);
                RefreshList();
            }
        }
    }

    // ── Visa kundens försäkringar vid klick ──────────────────────────────────
    private async void OnCustomerSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Customer customer)
            return;

        // LINQ: hämta försäkringar för vald kund
        var insurances = _vm.GetByCustomer(customer.Id).ToList();

        string info = insurances.Count == 0
            ? "Inga försäkringar registrerade."
            : string.Join("\n", insurances.Select(i =>
                $"• {i.InsuranceNumber}  {i.Type}  –  {i.CostDisplay}  [{i.StatusText}]"));

        await DisplayAlert($"Försäkringar – {customer.Name}", info, "Stäng");

        // Avmarkera raden
        ((CollectionView)sender).SelectedItem = null;
    }

    private void ClearForm()
    {
        EntryName.Text           = string.Empty;
        EntryEmail.Text          = string.Empty;
        EntryPhone.Text          = string.Empty;
        EntryPersonalNumber.Text = string.Empty;
        EntryAddress.Text        = string.Empty;
        EntryCity.Text           = string.Empty;
    }
}
