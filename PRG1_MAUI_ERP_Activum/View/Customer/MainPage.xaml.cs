using PRG1_MAUI_ERP_Activum.ViewModels;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class MainPage : ContentPage
{
    private readonly InsuranceViewModel _vm = InsuranceViewModel.Instance;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSearchCompleted(object sender, EventArgs e) => PerformSearch();
    private void OnSearchClicked(object sender, EventArgs e)   => PerformSearch();

    // ── Sök kund via ID eller personnummer (LINQ) ────────────────────────────
    private void PerformSearch()
    {
        string input = CustomerIdEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            InsuranceStatusLabel.Text      = "Ingen kund angiven.";
            InsuranceStatusLabel.TextColor = Colors.Red;
            return;
        }

        // LINQ: sök på kund-ID eller personnummer
        var customer = _vm.Customers.FirstOrDefault(c =>
            c.Id.ToString() == input ||
            c.PersonalNumber.Replace("-", "").Contains(input.Replace("-", "")));

        if (customer is null)
        {
            InsuranceStatusLabel.Text      = "Kund saknas i registret.";
            InsuranceStatusLabel.TextColor = Colors.OrangeRed;
            return;
        }

        // LINQ: hämta aktiva försäkringar för kunden
        var insurances = _vm.GetByCustomer(customer.Id).ToList();
        int activeCount = insurances.Count(i => i.IsActive);

        InsuranceStatusLabel.Text = activeCount == 0
            ? $"{customer.Name} – inga aktiva försäkringar."
            : $"{customer.Name} – {activeCount} aktiv{(activeCount == 1 ? "" : "a")} försäkring{(activeCount == 1 ? "" : "ar")}: " +
              string.Join(", ", insurances.Where(i => i.IsActive).Select(i => i.Type));

        InsuranceStatusLabel.TextColor = activeCount > 0 ? Colors.Green : Colors.Gray;
    }

    private async void OnSaveNotesClicked(object sender, EventArgs e)
    {
        string notes = NotesEditor?.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(notes))
        {
            await DisplayAlert("Tomt", "Ange anteckningstext innan du sparar.", "OK");
            return;
        }

        await DisplayAlert("Sparat", "Anteckningen har sparats.", "OK");
        NotesEditor.Text = string.Empty;
    }
}
