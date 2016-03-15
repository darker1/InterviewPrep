using System;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class SquareRoot : Runable, IQuestion<int, Decimal>
    {
        public ExpectedInputAndOutput<int, Decimal>[] TestValues {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<int, decimal>() {Input = 0, Output = 1.0M},
                    new ExpectedInputAndOutput<int, decimal>() {Input = 1, Output = 1.0M},
                    new ExpectedInputAndOutput<int, decimal>() {Input = 26, Output = 5.09901M, RoundDecimalPlaces = 5},
                    new ExpectedInputAndOutput<int, decimal>() {Input = 50, Output = 7.07107M, RoundDecimalPlaces = 5},
                };
            }
        }

        private const int MaxSqrt = 46340;
        private const decimal Tolerance = .0001M;

        public Decimal Run(int input)
        {
            if (input == 0)
                return 1.0M;

            if (input == 1)
                return 1.0M;


            int upper = EstimateUpperBound(input, 0, MaxSqrt);
            return EstimateDecimalPart(input, upper - 1, upper);
        }

        private int EstimateUpperBound(int value, int lower, int upper)
        {
            int midPoint = (int) ((lower + upper)/2);

            int doubledValue = midPoint*midPoint;
            int upperDoubledValue = (midPoint + 1)*(midPoint + 1);

            if (upperDoubledValue > value && doubledValue < value)
                return midPoint + 1;

            if (value > doubledValue)
            {
                return EstimateUpperBound(value, midPoint, upper);
            }
            else if(value < doubledValue) 
            {
                //break out slightly quicker
                if((midPoint - 1) * (midPoint - 1) < value)
                    return midPoint;
                
                return EstimateUpperBound(value, lower, midPoint);
            }

            return midPoint;
        }

        private decimal EstimateDecimalPart(int value, decimal lower, decimal upper)
        {
            decimal midpoint  = ((lower + upper) / 2);

            decimal midpointDoubled = midpoint*midpoint;

            if(value + Tolerance > midpointDoubled && value - Tolerance < midpointDoubled)
                return midpoint;

            if (value + Tolerance > midpointDoubled)
            {
                return EstimateDecimalPart(value, midpoint, upper);
            }
            else if ( value - Tolerance < midpointDoubled)
            {
                return EstimateDecimalPart(value, lower, midpoint);
            }

            return midpoint;
        }

        public string QuestionName {
            get { return "Given a number N, compute the SquareRoot of N to within .0001 tolerance"; }
        }
    }
}
