namespace DOBInput
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnValidateButtonClicked(object sender, EventArgs e)
        {
           string enteredDate = dateOfBirthEntry.Text;

           if (string.IsNullOrWhiteSpace(enteredDate) || enteredDate.Contains("_"))
           {
               validationLabel.Text = "❌ Please enter a complete date";
               validationLabel.TextColor = Colors.Red;
               resultPanel.IsVisible = false;
               return;
           }

           if (DateTime.TryParseExact( enteredDate, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth))
           {
               if (dateOfBirth > DateTime.Now)
               {
                   validationLabel.Text = "❌ Date of birth cannot be in the future";
                   validationLabel.TextColor = Colors.Red;
                   resultPanel.IsVisible = false;
                   return;
               }

               int age = DateTime.Now.Year - dateOfBirth.Year;

               if (dateOfBirth.Date > DateTime.Now.AddYears(-age))
               {
                   age--;
               }

               if (age < 13)
               {
                   validationLabel.Text = "❌ You must be at least 13 years old";
                   validationLabel.TextColor = Colors.Red;
                   resultPanel.IsVisible = false;
                   return;
               }

               validationLabel.Text = "✅ Valid date of birth";
               validationLabel.TextColor = Colors.Green;

               resultLabel.Text = $"Date of Birth: {dateOfBirth:MMMM dd, yyyy}\n" + $"Age: {age} years old";
               resultPanel.IsVisible = true;
           }
           else
           {
               validationLabel.Text = "❌ Invalid date format";
               validationLabel.TextColor = Colors.Red;
               resultPanel.IsVisible = false;
           }
       }
    }
}
