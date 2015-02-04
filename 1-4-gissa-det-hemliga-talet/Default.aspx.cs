using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_4_gissa_det_hemliga_talet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                SecretNumber secretNumber;
                int parsedGuessedNumber;
                CustomValidator customValidator;

                // Create Class instance if needed
                if (Session["secretNumber"] == null)
                {
                    Session["secretNumber"] = new SecretNumber();
                }

                // Try to parse user input, else throw an error.
                if (!int.TryParse(UserInputTextBox.Text, out parsedGuessedNumber))
                {
                    // Add custom error to Validationsummary
                    customValidator = new CustomValidator();
                    customValidator.IsValid = false;
                    customValidator.ErrorMessage = "Den inmatade värdet kunde inte tolkas som ett heltal.";
                    this.Page.Validators.Add(customValidator);

                    return;
                }

                // Make guess
                secretNumber.MakeGuess(parsedGuessedNumber);



                OutputLiteral.Text = secretNumber.Outcome.ToString() + " " + secretNumber.Count;

                OutputPanel.Visible = true;
            }
        }
    }
}