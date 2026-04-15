using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CustomersPage : ContentPage
{
    List<Customers> myCustomersList = new List<Customers>();

    public CustomersPage()
    {
        InitializeComponent();
    }

    public async void ADDCustomerButton(object sender, EventArgs e)
    {
        Customers customerObject = new Customers();
        customerObject.FirstName = EntryCustomerFirstName.Text;
        customerObject.SecondName = EntryCustomerSecondName.Text;
        customerObject.Mail = EntryCustomerMail.Text;

        var personal = EntryCustomerPersonalNumber?.Text?.Trim();
        if (string.IsNullOrWhiteSpace(personal))
        {
            await DisplayAlert("Validation", "incomplet", "OK");
            return;
        }
        if (!personal.All(char.IsDigit))
        {
            await DisplayAlert("Validation", "Felaktigt personnummer!", "OK");
            return;
        }
        customerObject.PersonalNumber = personal;

        var phone = EntryCustomerPhoneNumber?.Text?.Trim();
        if (string.IsNullOrWhiteSpace(phone))
        {
            await DisplayAlert("Validation", "incomplet", "OK");
            return;
        }
        if (!phone.All(char.IsDigit))
        {
            await DisplayAlert("Validation", "Felaktigt telefonnummer!", "OK");
            return;
        }
        customerObject.PhoneNumber = phone;
        myCustomersList.Add(customerObject);
        foreach (var item in myCustomersList)
            Debug.WriteLine($"{item.FirstName} {item.SecondName}: {item.Mail} {item.PersonalNumber} {item.PhoneNumber}");
    }

    public async void ShowCustomerButton(object sender, EventArgs e)
    {
        if (myCustomersList.Count == 0)
        {
            await DisplayAlert("Info", "Inga kunder finns", "OK");
            return;
        }

        string customersText = "";

        foreach (var customer in myCustomersList)
        {
            customersText += $"{customer.FirstName} {customer.SecondName}\n";
        }

        await DisplayAlert("Kunder", customersText, "OK");
    }

    public async void RemoveCustomerButton(object sender, EventArgs e)
    {
        if ((myCustomersList.Count == 0))
        {
            await DisplayAlert("Info", "Listan är tom", "OK");
            return;
        }

        var lastCustomer = myCustomersList.Last();
        myCustomersList.Remove(lastCustomer);

        await DisplayAlert("Borttagen", $"{lastCustomer.FirstName} togs bort", "OK");
    }
}