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
        private SecretNumber StoredSecretNumberObj
        {
            get
            {
                // Store a new SecretNumber object if needed
                if (Session["secretNumber"] == null)
                {
                    Session["secretNumber"] = new SecretNumber();
                }

                // Cast stored object to SecretNumber and return
                return (SecretNumber) Session["secretNumber"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                int parsedGuessedNumber;
                CustomValidator customValidator;

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
                this.StoredSecretNumberObj.MakeGuess(parsedGuessedNumber);

                this.DisplayFeedback();
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            // Reset Secret number object
            this.StoredSecretNumberObj.Initialize();

            // Hide output
            OutputPlaceHolder.Visible = false;

            // Reset text
            GuessHistory.Text = "";
            GuessStatus.Text = "";
            CurrentGuess.Text = "";

            // Reset controllers
            UserInputTextBox.Enabled = true;
            SubmitButton.Enabled = true;
            ResetButton.Visible = false;
        }

        protected void DisplayFeedback()
        {

            // If there are previous guesses.
            if (this.StoredSecretNumberObj.PreviousGuesses.Count() > 0)
            {
                // Output guess history, all but the last one.
                GuessHistory.Text = String.Join(", ", this.StoredSecretNumberObj.PreviousGuesses.Take(this.StoredSecretNumberObj.PreviousGuesses.Count() - 1).ToArray());

                // Output last guess from guess history
                CurrentGuess.Text = this.StoredSecretNumberObj.PreviousGuesses.Last().ToString();
            }
            else
            {
                // Output last guess from input textbox, prevent errors when reloading page as the game is over.
                CurrentGuess.Text = UserInputTextBox.Text;
            }


            // Check outcome
            if (Outcome.Correct == this.StoredSecretNumberObj.Outcome)
            {
                SubmitButton.Enabled = false;
                UserInputTextBox.Enabled = false;
                ResetButton.Visible = true;

                GuessStatus.CssClass = "success-message";
                GuessStatus.Text = String.Format("&#10003; Grattis! Du klarade det på {0} försök!", this.StoredSecretNumberObj.Count);
            }
            else if (Outcome.NoMoreGuesses == this.StoredSecretNumberObj.Outcome || !this.StoredSecretNumberObj.CanMakeGuess)
            {
                SubmitButton.Enabled = false;
                UserInputTextBox.Enabled = false;
                ResetButton.Visible = true;

                GuessStatus.CssClass = "error-message";
                GuessStatus.Text = String.Format("&#10007; Du har inga gissningar kvar. Det hemliga talet var {0}.", this.StoredSecretNumberObj.Number);
            }
            else if (Outcome.High == this.StoredSecretNumberObj.Outcome)
            {
                GuessStatus.CssClass = "error-message";
                GuessStatus.Text = "&#8679; För högt!";
            }
            else if (Outcome.Low == this.StoredSecretNumberObj.Outcome)
            {
                GuessStatus.CssClass = "error-message";
                GuessStatus.Text = "&#8681; För lågt!";
            }
            else if (Outcome.PreviousGuess == this.StoredSecretNumberObj.Outcome)
            {
                GuessStatus.CssClass = "error-message";
                GuessStatus.Text = "&#10007; Du har redan gissat på det här talet.";
            }

            OutputPlaceHolder.Visible = true;
        }
    }
}