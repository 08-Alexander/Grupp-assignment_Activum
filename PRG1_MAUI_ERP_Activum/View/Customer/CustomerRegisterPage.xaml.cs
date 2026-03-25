using PRG1_MAUI_ERP_Activum.Models;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Customers;

public partial class CustomerRegisterPage : ContentPage
{
    public CustomerRegisterPage()
    {
        InitializeComponent();
        LoadCustomers();
    }

    private void LoadCustomers()
    {
        CustomerList.ItemsSource = null;
        CustomerList.ItemsSource = CustomerRegister.Customers;
    }

    private void OnAddCustomer(object sender, EventArgs e)
    {
        var customer = new Models.Customer
        {
            Name = "Ny kund",
            Email = "kund@mail.se",
            Phone = "0701234567",
            PersonalNumber = "19900101-1234"
        };

        CustomerRegister.Add(customer);
        LoadCustomers();
    }
}