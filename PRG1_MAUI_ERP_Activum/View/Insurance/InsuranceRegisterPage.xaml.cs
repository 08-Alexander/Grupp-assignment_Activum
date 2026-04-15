using PRG1_MAUI_ERP_Activum.Models;
using PRG1_MAUI_ERP_Activum.ViewModels;

namespace PRG1_MAUI_ERP_Activum.View.Insurance;

public partial class InsuranceRegisterPage : ContentPage
{
    private readonly InsuranceViewModel _vm = InsuranceViewModel.Instance;

    public InsuranceRegisterPage()
    {
        InitializeComponent();
        PickerStatusFilter.SelectedIndex = 0;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Uppdatera kundpickern om nya kunder har lagts till
        PickerCustomer.ItemsSource = _vm.Customers
            .OrderBy(c => c.Name)
            .Select(c => $"{c.Name}  (ID {c.Id})")
            .ToList();

        RefreshList();
    }

    // ── LINQ: filtrera och sortera listan ────────────────────────────────────
    private void RefreshList()
    {
        string search = EntrySearch?.Text?.Trim().ToLower() ?? string.Empty;
        string status = PickerStatusFilter?.SelectedItem?.ToString() ?? "Alla";

        var result = _vm.Insurances
            .Where(i =>
                (string.IsNullOrEmpty(search) ||
                 i.Type.ToLower().Contains(search) ||
                 i.InsuranceNumber.ToLower().Contains(search) ||
                 i.CustomerId.ToString().Contains(search) ||
                 i.Description.ToLower().Contains(search)) &&
                status switch
                {
                    "Aktiva"   => i.IsActive,
                    "Inaktiva" => !i.IsActive,
                    _          => true
                })
            .OrderBy(i => i.Type)
            .ThenBy(i => i.InsuranceNumber)
            .ToList();

        InsuranceList.ItemsSource = result;
    }

    // ── Lägg till försäkring ─────────────────────────────────────────────────
    private async void OnAddInsuranceClicked(object sender, EventArgs e)
    {
        if (PickerCustomer.SelectedIndex < 0)
        {
            await DisplayAlert("Obligatoriskt fält", "Välj en kund.", "OK");
            return;
        }
        if (PickerType.SelectedIndex < 0)
        {
            await DisplayAlert("Obligatoriskt fält", "Välj försäkringstyp.", "OK");
            return;
        }
        if (!decimal.TryParse(EntryCost.Text, out decimal cost) || cost <= 0)
        {
            await DisplayAlert("Ogiltigt värde", "Ange en giltig månadskostnad.", "OK");
            return;
        }

        // Hämta vald kund från sorterad lista (matchar picker-index)
        var selectedCustomer = _vm.Customers
            .OrderBy(c => c.Name)
            .ElementAt(PickerCustomer.SelectedIndex);

        var insurance = new PRG1_MAUI_ERP_Activum.Models.Insurance
        {
            CustomerId  = selectedCustomer.Id,
            Type        = PickerType.SelectedItem!.ToString()!,
            RiskLevel   = PickerRisk.SelectedItem?.ToString() ?? "Medel",
            Description = EntryDescription.Text?.Trim() ?? string.Empty,
            MonthlyCost = cost,
            StartDate   = DateTime.Today,
            IsActive    = true
        };

        _vm.AddInsurance(insurance);

        // Rensa formulär
        PickerCustomer.SelectedIndex = -1;
        PickerType.SelectedIndex     = -1;
        PickerRisk.SelectedIndex     = -1;
        EntryDescription.Text        = string.Empty;
        EntryCost.Text               = string.Empty;

        RefreshList();
    }

    // ── Ta bort försäkring ───────────────────────────────────────────────────
    private async void OnRemoveInsuranceClicked(object sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: PRG1_MAUI_ERP_Activum.Models.Insurance insurance })
        {
            bool confirm = await DisplayAlert(
                "Ta bort försäkring",
                $"Vill du ta bort {insurance.InsuranceNumber} ({insurance.Type})?",
                "Ja", "Avbryt");

            if (confirm)
            {
                _vm.RemoveInsurance(insurance);
                RefreshList();
            }
        }
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e) => RefreshList();
    private void OnFilterChanged(object sender, EventArgs e)            => RefreshList();

    private void OnResetClicked(object sender, EventArgs e)
    {
        EntrySearch.Text = string.Empty;
        PickerStatusFilter.SelectedIndex = 0;
        RefreshList();
    }
}
