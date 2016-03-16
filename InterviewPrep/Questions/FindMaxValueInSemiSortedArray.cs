using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class FindMaxValueInSemiSortedArray : Runable, IQuestion<int[], int>
    {
        public string QuestionName
        {
            get { return "Find the max value in an array. The array is semi-sorted, meaning it will increase to a point and then decrease (or just increase, or just decrease)."; }
        }

        public ExpectedInputAndOutput<int[], int>[] TestValues
        {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{5}, Output = 5},
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{1,2,5,2,1}, Output = 5},
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{1,2,5,1}, Output = 5},
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{1, 3, 4, 7, 9, 10, 12, 13, 12, 6, 3}, Output = 13},
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{1, 3, 4, 7, 9, 10, 12, 13}, Output = 13},
                    new ExpectedInputAndOutput<int[], int>() {Input = new []{13, 12, 10, 9, 7, 4, 2, 1}, Output = 13},
                };
            }
        }

        public int Run(int[] input)
        {
            if (input == null || input.Length == 0)
                throw new Exception("Cannot find max value in an empty/null array.");

            if (input.Length == 1)
                return input[0];

            return FindMaxBinarySearch(input, 0, input.Length - 1);
        }

        private int FindMaxBinarySearch(int[] input, int left, int right)
        {
            if (left == right)
            {
                return input[right];
            }

            int midPoint = (left + right) / 2;

            if (input[midPoint] > input[midPoint + 1] && input[midPoint] > input[midPoint - 1])
                return input[midPoint];

            if (input[midPoint] < input[midPoint + 1])
                return FindMaxBinarySearch(input, midPoint + 1, right);

            return FindMaxBinarySearch(input, left, midPoint - 1);
        }

        public int FirstRun(int[] input)
        {
            int left = int.MinValue;
            int right = int.MinValue;

            if (input == null || input.Length == 0)
                throw new Exception("Cannot find max value in an empty/null array.");

            if (input.Length == 1)
                return input[0];

            for (int i = 0; i < input.Length / 2; i++)
            {
                if (left < input[i])
                    left = input[i];
                else
                    return left;

                var rightIndex = input.Length - 1 - i;
                if (right < input[rightIndex])
                    right = input[rightIndex];
                else
                    return right;
            }

            return input[input.Length / 2];
        }
    }
}
