namespace PRG1_MAUI_ERP_Activum.View;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
	}

    private double accumulator = 0;
    private double operand = 0;
    private string operation = "";

    // hantering för numeriska knappar
    private void NumberButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        // Bygg upp operand baserat på knapptexten (t.ex. "1", "2")
        operand = (operand * 10) + Convert.ToDouble(button.Text);

        EntryCalculations.Text += button.Text;
        EntryResult.Text = operand.ToString();
    }


    // hantering för operator-knappar (+, -, *, /)
    private void OperatorButton(object sender, EventArgs e)
    {
        if (operation != "") // Utför beräkning om en tidigare operation finns
        {
            Calculate();
        }
        else
        {
            accumulator = operand; // Spara första talet i accumulator
        }

        operand = 0;

        Button button = (Button)sender;
        operation = button.Text;

        EntryCalculations.Text += $" {operation} ";
    }


    private void EqualButton(object sender, EventArgs e)
    {
        Calculate();

        EntryResult.Text = accumulator.ToString();
        EntryCalculations.Text = accumulator.ToString();

        operation = "";
        operand = 0;
    }


    private void Calculate()
    {
        switch (operation)
        {
            case "+":
                accumulator += operand;
                break;
            case "-":
                accumulator -= operand;
                break;
            case "*":
                accumulator *= operand;
                break;
            case "/":
                if (operand == 0) // Hantera division med noll
                {
                    DisplayAlertAsync("Fel!", "Division med noll är ej tillåtet.", "OK");
                    Clear();
                    return;
                }
                accumulator /= operand;
                break;
        }

        operand = 0;
    }

    private void ClearButton(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        accumulator = 0;
        operand = 0;
        operation = "";

        EntryCalculations.Text = "";
        EntryResult.Text = "0";
    }

    private void StoreInMemoryButton(object sender, EventArgs e)
    {
        EntryCalculations.Text = "Kommande funktion";
    }

    private void CatchFromMemoryButton(object sender, EventArgs e)
    {
        EntryCalculations.Text = "Kommande funktion";
    }

}