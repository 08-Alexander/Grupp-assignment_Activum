using System.Threading.Tasks;


namespace PRG1_MAUI_ERP_Activum.View;

public partial class PerDiemPage : ContentPage
{
    public PerDiemPage()
    {
        InitializeComponent();
    }

    public async void PerDiem_Clicked(object sender, EventArgs e)
    {
        string[] storage =
        {
            ReasonEntry.Text,
            DaysEntry.Text,
            DateEntry.Text,
            EndDateEntry.Text,
            TravelToolEntry.Text,
            OtherExpensesEntry.Text
        };

        if (storage.Any(string.IsNullOrWhiteSpace))
        {
            await DisplayAlert("Fel", "Dokumentet är tomt någonstans", "OK");
            return;
        }

        await DisplayAlert("Skickat", "Tack för att du skickade in din ansökan", "Klart");

    }

}