namespace PRG1_MAUI_ERP_Activum.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSearchCompleted(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            PerformSearch();
        }


        // TODO Sökfunktionen på startsidan är inte implementerad
        private void PerformSearch()
        {
            string input = CustomerIdEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                InsuranceStatusLabel.Text = "Ingen kund angiven.";
                InsuranceStatusLabel.TextColor = Colors.Red;
                return;
            }

            bool custumerFound = LookupCustomer(input);

            if (custumerFound)
            {
                // TODO Det finns ingen lista över vare sig kunder eller försäkringar. Ändra!
                InsuranceStatusLabel.Text = "Kund hittad — har 2 aktiva försäkringar.";
                InsuranceStatusLabel.TextColor = Colors.Green;
            }
            else
            {
                InsuranceStatusLabel.Text = "Kund saknas i registret.";
                InsuranceStatusLabel.TextColor = Colors.OrangeRed;
            }
        }

        private bool LookupCustomer(string input)
        {
            // TODO just: alla inputs på startsidan som slutar på "1" anses existera. Ändra!
            return input.EndsWith("1");
        }

        private async void OnSaveNotesClicked(object sender, EventArgs e)
        {
            string notes = NotesEditor.Text?.Trim();
            DateTime? date = DatePickerField.Date;

            if (string.IsNullOrWhiteSpace(notes))
            {
                await DisplayAlertAsync("Fel", "Anteckningen är tom.", "OK");
                return;
            }

            // TODO Skadeanmälan sparas inte just nu.
            await DisplayAlertAsync("Sparat", $"Anteckning sparad för datum: {date:d}", "OK");
        }
    }
}
