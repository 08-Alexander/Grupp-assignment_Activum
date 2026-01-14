using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View.Customer;

public partial class CalculatorPage : ContentPage
{
    private double _currentValue = 0;
    private string _lastOperator = "";
    private bool _isNewInput = true;
    private double _memoryValue = 0;

    // För admin-info
    private double _lastOriginalValue;
    private double _lastNewValue;

    public CalculatorPage()
    {
        InitializeComponent();
        Display.Text = "0";
        SetupRoleBasedUI();
    }

    private void SetupRoleBasedUI()
    {
        AdminDetailsPanel.IsVisible = AppState.UserRole == "Employee";
    }

    private void Number_Click(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (_isNewInput)
        {
            Display.Text = button.Text;
            _isNewInput = false;
        }
        else
        {
            Display.Text += button.Text;
        }
    }

    private void Operation_Click(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (double.TryParse(Display.Text, out double value))
        {
            _currentValue = value;
            _lastOperator = button.Text;
            _isNewInput = true;
        }
    }

    private void Calculate()
    {
        if (!double.TryParse(Display.Text, out double newValue))
            return;

        _lastOriginalValue = _currentValue;
        _lastNewValue = newValue;

        switch (_lastOperator)
        {
            case "+": _currentValue += newValue; break;
            case "-": _currentValue -= newValue; break;
            case "×": _currentValue *= newValue; break;
            case "÷":
                if (newValue == 0) { ShowError(); return; }
                _currentValue /= newValue;
                break;
            case "%": _currentValue = (_currentValue * newValue) / 100; break;
            case "√":
                if (_currentValue < 0) { ShowError(); return; }
                _currentValue = Math.Sqrt(_currentValue);
                break;
            case "^": _currentValue = Math.Pow(_currentValue, newValue); break;
            case "1/x":
                if (newValue == 0) { ShowError(); return; }
                _currentValue = 1 / newValue;
                break;
        }

        Display.Text = _currentValue.ToString();
        HistoryLabel.Text = $"{_lastOriginalValue} {_lastOperator} {_lastNewValue} = {_currentValue}";

        if (AppState.UserRole == "Employee")
        {
            CalculationInfoLabel.Text =
                $"Uttryck: {_lastOriginalValue} {_lastOperator} {_lastNewValue}\nResultat: {_currentValue}";
        }
    }

    private void Equals_Click(object sender, EventArgs e)
    {
        Calculate();
        _isNewInput = true;
        _lastOperator = "";
    }

    private void Clear_Click(object sender, EventArgs e)
    {
        Display.Text = "0";
        _currentValue = 0;
        _isNewInput = true;
    }

    private void ClearEntry_Click(object sender, EventArgs e)
    {
        Display.Text = "0";
        _isNewInput = true;
    }

    private void Backspace_Click(object sender, EventArgs e)
    {
        if (Display.Text.Length > 1)
            Display.Text = Display.Text[..^1];
        else
            Display.Text = "0";
    }

    private void PlusMinus_Click(object sender, EventArgs e)
    {
        if (double.TryParse(Display.Text, out double value))
            Display.Text = (-value).ToString();
    }

    private void Decimal_Click(object sender, EventArgs e)
    {
        if (!Display.Text.Contains("."))
            Display.Text += ".";
    }

    private void ShowError()
    {
        Display.Text = "Error";
        _isNewInput = true;
    }

    private void MemoryClear_Click(object sender, EventArgs e) => _memoryValue = 0;
    private void MemoryRecall_Click(object sender, EventArgs e) => Display.Text = _memoryValue.ToString();
    private void MemoryAdd_Click(object sender, EventArgs e)
    {
        if (double.TryParse(Display.Text, out double value))
            _memoryValue += value;
    }

    private void MemorySubtract_Click(object sender, EventArgs e)
    {
        if (double.TryParse(Display.Text, out double value))
            _memoryValue -= value;
    }
}
