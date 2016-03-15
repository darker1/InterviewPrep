using System;

namespace InterviewPrep.Helpers
{
    public class ExpectedInputAndOutput<TInput, TOutput> where TOutput : IComparable
    {
        private int _roundDecimalPlaces;
        private bool _shouldRound;
        public TInput Input { get; set; }
        public TOutput Output { get; set; }

        public int TicksAllowedPerAttempt { get; set; }

        public int RoundDecimalPlaces
        {
            get { return _roundDecimalPlaces; }
            set
            {
                if (typeof (TOutput) != typeof (decimal) && typeof (TOutput) != typeof (Double))
                    throw new TypeLoadException("Cannot use RoundDecimalPlaces without Output being decimal or double.");
                _shouldRound = true;
                _roundDecimalPlaces = value;
            }
        }

        public bool ShouldRoundOutput {
            get { return _shouldRound; }
        }

        public ExpectedInputAndOutput()
        {
            if (RoundDecimalPlaces > 0)
            {
             }
        }

    }

}
