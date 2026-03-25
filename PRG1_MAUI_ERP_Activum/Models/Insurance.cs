using System;
using System.Collections.Generic;
using System.Text;

namespace PRG1_MAUI_ERP_Activum.Models;

public class Insurance
{
    public int Id { get; set; }

    public string InsuranceNumber { get; set; }

    public string Type { get; set; }

    public decimal MonthlyCost { get; set; }

    public int CustomerId { get; set; }
    public string RiskLevel { get; internal set; }
}
