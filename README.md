# How-to-Add-Date-of-Birth-Input-with-Mask-Validation-in-.NET-MAUI-MaskedEntry

This section explains how to create a Date of Birth (DOB) input form in .NET MAUI using the SfMaskedEntry control. By applying a date mask, users can enter their date of birth in a consistent MM/DD/YYYY format while preventing invalid input patterns. The entered date is validated to ensure it is complete, not a future date, and meets a minimum age requirement before displaying the calculated age.

# XAML

Define an SfMaskedEntry control with a date mask that automatically guides users to enter dates in the MM/DD/YYYY format. A validation label, submit button, and result panel are included to display validation feedback and calculated age information.

```xml
<ScrollView>
    <VerticalStackLayout Padding="30"
                         Spacing="20">

        <!-- Title -->
        <Label Text="Date of Birth"
               FontSize="24"
               FontAttributes="Bold"
               TextColor="#1f1f1f"
               Margin="0,20,0,0" />

        <!-- Description -->
        <Label Text="Enter your date of birth in MM/DD/YYYY format"
               FontSize="13"
               TextColor="#666666"
               Margin="0,0,0,10" />

        <!-- Masked Entry for Date of Birth -->
        <editors:SfMaskedEntry x:Name="dateOfBirthEntry"
                               Mask="00/00/0000"
                               PromptChar="_"
                               Placeholder="MM/DD/YYYY"
                               Padding="10"
                               FontSize="16"
                               TextColor="#1f1f1f"
                               VerticalOptions="Start"
                               HorizontalOptions="Fill"
                               HeightRequest="50">

            <editors:SfMaskedEntry.Background>
                <SolidColorBrush Color="White" />
            </editors:SfMaskedEntry.Background>

        </editors:SfMaskedEntry>

        <!-- Validation Result -->
        <Label x:Name="validationLabel"
               Text=""
               FontSize="13"
               Margin="0,10,0,0" />

        <!-- Submit Button -->
        <Button Text="Validate Date"
                Clicked="OnValidateButtonClicked"
                Padding="15,12"
                CornerRadius="8"
                FontSize="16"
                FontAttributes="Bold"
                BackgroundColor="#2E7D32"
                TextColor="White"
                Margin="0,20,0,0" />

        <!-- Result Display -->
        <VerticalStackLayout x:Name="resultPanel"
                             IsVisible="False"
                             Padding="15"
                             Spacing="10"
                             BackgroundColor="White"
                             Margin="0,20,0,0">

            <Label Text="Validation Result:"
                   FontSize="14"
                   FontAttributes="Bold"
                   TextColor="#1f1f1f" />

            <Label x:Name="resultLabel"
                   Text=""
                   FontSize="13"
                   TextColor="#333333"
                   LineBreakMode="WordWrap" />

        </VerticalStackLayout>

    </VerticalStackLayout>
</ScrollView>
```

# C#

Handle the button click event to validate the entered date. The implementation verifies that the mask is completely filled, validates the date format, checks whether the date is in the future, calculates the user's age, and ensures that the user meets the minimum age requirement.

```csharp
private void OnValidateButtonClicked(object sender,EventArgs e)
{
    string enteredDate = dateOfBirthEntry.Text;

    if (string.IsNullOrWhiteSpace(enteredDate) || enteredDate.Contains("_"))
    {
        validationLabel.Text =
            "❌ Please enter a complete date";

        validationLabel.TextColor = Colors.Red;
        resultPanel.IsVisible = false;
        return;
    }

    if (DateTime.TryParseExact( enteredDate, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth))
    {
        if (dateOfBirth > DateTime.Now)
        {
            validationLabel.Text =
                "❌ Date of birth cannot be in the future";

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
            validationLabel.Text =
                "❌ You must be at least 13 years old";

            validationLabel.TextColor = Colors.Red;
            resultPanel.IsVisible = false;
            return;
        }

        validationLabel.Text =
            "✅ Valid date of birth";

        validationLabel.TextColor = Colors.Green;

        resultLabel.Text = $"Date of Birth: {dateOfBirth:MMMM dd, yyyy}\n" + $"Age: {age} years old";

        resultPanel.IsVisible = true;
    }
    else
    {
        validationLabel.Text =
            "❌ Invalid date format";

        validationLabel.TextColor = Colors.Red;
        resultPanel.IsVisible = false;
    }
}
```

The SfMaskedEntry control simplifies date entry by enforcing a predefined MM/DD/YYYY format and preventing incomplete input. Combined with custom validation logic, it ensures that users enter a valid date of birth, prevents future dates, verifies age requirements, and provides immediate feedback. This approach improves data accuracy while creating a more guided and user-friendly data entry experience.