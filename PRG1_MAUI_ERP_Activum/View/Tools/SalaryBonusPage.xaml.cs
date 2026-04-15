namespace PRG1_MAUI_ERP_Activum.View.Tools;

public partial class SalaryBonusPage : ContentPage
{
    private const double BaseSalary = 26000;
    private const double CommissionRate = 0.12;
    private const double TaxRate = 0.32;
    private List<double> _monthlySalaries = new();

    public SalaryBonusPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(SalesEntry.Text, out double sales))
        {
            DisplayAlert("Fel", "Ange giltig försäljning", "OK");
            return;
        }

        double commission = sales * CommissionRate;

        double grossSalary = BaseSalary + commission;

        double tax = grossSalary * TaxRate;

        double netSalary = grossSalary - tax;

        _monthlySalaries.Add(grossSalary);

        if (_monthlySalaries.Count > 11)
            _monthlySalaries.RemoveAt(0);

        double vacationPay = _monthlySalaries.Average();

        ProvisionLabel.Text = $"Provision: {commission:0} kr";
        GrossSalaryLabel.Text = $"Bruttolön: {grossSalary:0} kr";
        TaxLabel.Text = $"Skatt: {tax:0} kr";
        NetSalaryLabel.Text = $"Nettolön: {netSalary:0} kr";
        VacationPayLabel.Text = $"Semesterlön: {vacationPay:0} kr";
    }
}
