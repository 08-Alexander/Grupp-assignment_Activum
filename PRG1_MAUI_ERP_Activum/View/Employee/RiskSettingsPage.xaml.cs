using Microsoft.Extensions.Logging.Abstractions;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Employee;

public partial class RiskSettingsPage : ContentPage
{
    public RiskSettingsPage()
    {
        InitializeComponent();

        LowEntry.Text = InsuranceConfigService.LowRiskFactor.ToString();
        MediumEntry.Text = InsuranceConfigService.MediumRiskFactor.ToString();
        HighEntry.Text = InsuranceConfigService.HighRiskFactor.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (double.TryParse(LowEntry.Text, out var low) &&
            double.TryParse(MediumEntry.Text, out var medium) &&
            double.TryParse(HighEntry.Text, out var high))
        {
            InsuranceConfigService.LowRiskFactor = low;
            InsuranceConfigService.MediumRiskFactor = medium;
            InsuranceConfigService.HighRiskFactor = high;

            await DisplayAlert("Sparat", "Riskfaktorer uppdaterade", "OK");
        }
        else
        {
            await DisplayAlert("Fel", "Ange giltiga siffror", "OK");
        }
    }
}
