using PRG1_MAUI_ERP_Activum.Models;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Insurance;

public partial class InsuranceRegisterPage : ContentPage
{
    public InsuranceRegisterPage()
    {
        InitializeComponent();
        LoadInsurances();
    }

    private void LoadInsurances()
    {
        InsuranceList.ItemsSource = null;
        InsuranceList.ItemsSource = InsuranceRegister.Insurances;
    }

    private void OnAddInsurance(object sender, EventArgs e)
    {
        var insurance = new PRG1_MAUI_ERP_Activum.Models.Insurance
        {
            Type = "Hemförsäkring",
            RiskLevel = "Medel",
            MonthlyCost = 350,
            CustomerId = 1
        };

        InsuranceRegister.Add(insurance);

        LoadInsurances();
    }
}