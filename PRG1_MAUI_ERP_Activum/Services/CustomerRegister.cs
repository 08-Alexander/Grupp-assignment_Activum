using System;
using System.Collections.Generic;
using System.Text;

using PRG1_MAUI_ERP_Activum.Models;

namespace PRG1_MAUI_ERP_Activum.Services;

public static class CustomerRegister
{
    public static List<Customer> Customers { get; } = new();

    public static void Add(Customer customer)
    {
        customer.Id = Customers.Count + 1;
        Customers.Add(customer);
    }

    public static Customer Get(int id)
    {
        return Customers.FirstOrDefault(c => c.Id == id);
    }
}
