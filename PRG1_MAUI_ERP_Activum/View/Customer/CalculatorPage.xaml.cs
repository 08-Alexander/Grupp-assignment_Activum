using Microsoft.Maui.Controls;

namespace PRG1_MAUI_ERP_Activum.View.Customer;

public partial class CalculatorPage : ContentPage
{
    private double _currentValue = 0;
    private string _lastOperator = "";
    private bool _isNewInput = true;
    private double _memoryValue = 0;
    private string _history = "";

    public CalculatorPage()
    {
        InitializeComponent();
        Display.Text = "0";
    }

    private void Number_Click(object sender, EventArgs e)
    {
        var button = sender as Button;

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
        var button = sender as Button;

        if (double.TryParse(Display.Text, out double result))
        {
            if (!string.IsNullOrEmpty(_lastOperator))
            {
                Calculate();
            }

            _currentValue = result;
            _lastOperator = button.Text;
            _isNewInput = true;
        }
    }

    private void Calculate()
    {
        if (!double.TryParse(Display.Text, out double newValue))
            return;

        double originalValue = _currentValue;

        switch (_lastOperator)
        {
            case "+":
                _currentValue += newValue;
                break;
            case "-":
                _currentValue -= newValue;
                break;
            case "×":
                _currentValue *= newValue;
                break;
            case "÷":
                if (newValue == 0)
                {
                    ShowError();
                    return;
                }
                _currentValue /= newValue;
                break;
            case "%":
                _currentValue = (_currentValue * newValue) / 100;
                break;
            case "√":
                if (_currentValue < 0)
                {
                    ShowError();
                    return;
                }
                _currentValue = Math.Sqrt(_currentValue);
                break;
            case "^":
                _currentValue = Math.Pow(originalValue, newValue);
                break;
            case "1/x":
                if (newValue == 0)
                {
                    ShowError();
                    return;
                }
                _currentValue = 1 / newValue;
                break;
        }

        _history = $"{originalValue} {_lastOperator} {newValue} = {_currentValue}\n{_history}";
        HistoryLabel.Text = _history;
        Display.Text = _currentValue.ToString();
    }

    private void Equals_Click(object sender, EventArgs e)
    {
        Calculate();
        _lastOperator = "";
        _isNewInput = true;
    }

    private void Clear_Click(object sender, EventArgs e)
    {
        Display.Text = "0";
        _currentValue = 0;
        _lastOperator = "";
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
        if (double.TryParse(Display.Text, out double result))
            Display.Text = (-result).ToString();
    }

    private void Decimal_Click(object sender, EventArgs e)
    {
        if (!Display.Text.Contains("."))
            Display.Text += ".";
    }

    private void ShowError()
    {
        Display.Text = "Error";
        _lastOperator = "";
        _isNewInput = true;
    }

    private void MemoryClear_Click(object sender, EventArgs e) => _memoryValue = 0;

    private void MemoryRecall_Click(object sender, EventArgs e) =>
        Display.Text = _memoryValue.ToString();

    private void MemoryAdd_Click(object sender, EventArgs e)
    {
        if (double.TryParse(Display.Text, out double result))
            _memoryValue += result;
    }

    private void MemorySubtract_Click(object sender, EventArgs e)
    {
        if (double.TryParse(Display.Text, out double result))
            _memoryValue -= result;
    }
}
