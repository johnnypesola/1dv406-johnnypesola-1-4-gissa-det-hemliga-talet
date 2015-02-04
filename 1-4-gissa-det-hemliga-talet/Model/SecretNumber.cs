using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace _1_4_gissa_det_hemliga_talet
{

    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Correct,
        NoMoreGuesses,
        PreviousGuess
    }

    public class SecretNumber
    {
        // Fields
        protected int _number;
        protected List<int> _previousGuesses;
        const int MAX_NUMBER_OF_GUESSES = 7;
        const int MIN_NUMBER = 1;
        const int MAX_NUMBER = 100;

        // Properties
        public bool CanMakeGuess {
            get {
                return this.Count < MAX_NUMBER_OF_GUESSES;
            }
        }

        public int Count
        {
            get
            {
                return this.PreviousGuesses.Count();
            }
        }

        public int? Number
        {
            get
            {
                if (this.CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return this._number;
                }
            }
            private set
            {
                if (value is int)
                {
                    this._number = (int) value;
                }
            }
        }

        public Outcome Outcome
        {
            get;
            private set;
        }

        public IEnumerable<int> PreviousGuesses
        {
            /*
             * PreviousGuesses ger en referens till en samling innehållande gjorda gissningar. Observera att den
                underliggande typen är ReadOnlyCollection<int>. För att undvika en ”privacy leak” av det privata
                fältet _previousGuesses ger egenskapen en ”read-only”-referens till List-objektet vilket omöjliggör
                manipulering av gjorda gissningar utanför affärslogikklassen.
             */
            get {
                return new ReadOnlyCollection<int>(this._previousGuesses);
            }
        }
        
        // Constructor
        public SecretNumber()
        {
            /*
             * Klassens enda konstruktor ser till att då ett objekt instansieras av klassen har ett giltigt slumptal, samt
                skapar en instans av List-objektet med plats för sju element som ska innehålla gjorda gissningar
                sedan det hemliga talet slumpats fram
             */

            // Create room for max number of guesses
            this._previousGuesses = new List<int>(MAX_NUMBER_OF_GUESSES);

            this.Initialize();            
        }

        // Methods
        public void Initialize() 
        {
            /*
             * Initialize initierar klassens medlemmar. _number tilldelas ett slumptal i det slutna intervallet
                mellan 1 och 100. Eventuella element i _previousGuesses tas bort genom anrop av metoden Clear.
                Egenskapen Outcome tilldelas värdet Indefinite.
             */

            // Generate new number to guess.
            Random random = new Random();
            this.Number = random.Next(MIN_NUMBER, MAX_NUMBER);

            // Clear old guesses.
            this._previousGuesses.Clear();

            // Set undefinite outcome
            this.Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            /*
             * MakeGuess returnerar ett värde av typen Outcome som indikerar om det gissade talet är rätt, för lågt,
                för högt, en tidigare gjord gissning eller om användaren förbrukat alla gissningar. Är inte gissningen i
                det slutna intervallet mellan 1 och 100 kastas undantaget ArgumentOutOfRangeException.
             */
            
            // Check if guess is out of range
            if (guess > MAX_NUMBER || guess < MIN_NUMBER) {
                throw new ArgumentOutOfRangeException(String.Format("guess must be higher than {0} and lower than {1}", MIN_NUMBER, MAX_NUMBER));
            }

            // Check if there are no guesses left.
            else if (!this.CanMakeGuess)
            {
                this.Outcome = Outcome.NoMoreGuesses;
            }
            // Check if this number is a previous guess.
            else if (PreviousGuesses.Contains(guess))
            {
                this.Outcome = Outcome.PreviousGuess;
            }
            // Check if guess is too low
            else if (guess < this._number)
            {
                this.Outcome = Outcome.Low;
            }
            // Check if guess is too high
            else if (guess > this._number)
            {
                this.Outcome = Outcome.High;
            }
            // Check if guess is correct
            else if (guess == this._number)
            {
                this.Outcome = Outcome.Correct;
            }

            return this.Outcome;
        }

    }
}