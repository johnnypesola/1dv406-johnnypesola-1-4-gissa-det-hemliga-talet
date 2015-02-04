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
            get {
                return new ReadOnlyCollection<int>(this._previousGuesses);
            }
        }
        
        // Constructor
        public SecretNumber()
        {
            // Create room for max number of guesses
            this._previousGuesses = new List<int>(MAX_NUMBER_OF_GUESSES);

            this.Initialize();            
        }

        // Methods
        public void Initialize() 
        {
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
            // Guess is out of range
            if (guess > MAX_NUMBER || guess < MIN_NUMBER) {
                throw new ArgumentOutOfRangeException(String.Format("guess must be higher than {0} and lower than {1}", MIN_NUMBER, MAX_NUMBER));
            }
            // No guesses left
            else if (!this.CanMakeGuess)
            {
                this.Outcome = Outcome.NoMoreGuesses;
            }
            // Guess is a previous guess
            else if (PreviousGuesses.Contains(guess))
            {
                this.Outcome = Outcome.PreviousGuess;
            }
            // Guess is correct
            else if (guess == this._number)
            {
                this.Outcome = Outcome.Correct;

                // Remember guess
                this._previousGuesses.Add(guess);
            }
            // Guess is too low
            else if (guess < this._number)
            {
                this.Outcome = Outcome.Low;

                // Remember guess
                this._previousGuesses.Add(guess);
            }
            // Guess is too high
            else if (guess > this._number)
            {
                this.Outcome = Outcome.High;

                // Remember guess
                this._previousGuesses.Add(guess);
            }

            return this.Outcome;
        }

    }
}