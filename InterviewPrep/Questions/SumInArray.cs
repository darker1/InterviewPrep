using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class MaxSumOfSequenceInArray : Runable, IQuestion<int[], int>
    {
        public ExpectedInputAndOutput<int[], int>[] TestValues {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {1,2,3,4,5,6,7,8,9}, Output = 45, TicksAllowedPerAttempt = 100},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,8,7,6,5,4,3,2,1}, Output = 45},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,8,7,6,5,4,3,2,1,0,-1}, Output = 45},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,7,5,1,0,-1}, Output = 9},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,8,6,5,4,3}, Output = 18},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {-1,0,1,2,3}, Output = 6},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9}, Output = 9},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {-2, 1, -3, 4, -1, 2, 1, -5, 4}, Output = 4}
                };
            }
        }

        private class Sequence
        {
            private List<int> _intSequence = new List<int>();

            public IEnumerable<int> IntSequence
            {
                get { return _intSequence; }
            }

            public int Total { get; private set; }

            public void Add(int i)
            {
                _intSequence.Add(i);
                if (i > 0)
                {
                    Total += i;
                }
                else
                {
                    if (Total < 0 && i > Total)
                        Total = i;
                }
            }

            public int Last {
                get { return _intSequence.Last(); }
            }

            public int First
            {
                get { return _intSequence.First(); }

            }

            public Sequence(int i)
            {
                Add(i);
            }
        }

        public int Run(int[] input)
        {
            if(input.Length == 0)
                throw new ArgumentException("Input cannot have 0 characters.");
            if (input.Length == 1)
                return input[0];

            var currentSequence = new Sequence(input[0]) ;
            var sequences = new List<Sequence>() { currentSequence };
            var maxSequence = currentSequence;

            for (int i = 1; i < input.Length; i++)
            {
                int difference = input[i] - currentSequence.Last;
                if ((difference == 1 && input[i] - currentSequence.First > 0) ||
                    (difference == -1 && input[i] - currentSequence.First < 0))
                {
                    currentSequence.Add(input[i]);
                }
                else
                {
                    currentSequence = new Sequence(input[i]);
                    sequences.Add(currentSequence);
                }
                if (currentSequence.Total > maxSequence.Total)
                    maxSequence = currentSequence;
            }

            return maxSequence.Total;
        }

        public string QuestionName {
            get { return "Maximum sum of a sequence of numbers in an array."; }
        }
    }
}
