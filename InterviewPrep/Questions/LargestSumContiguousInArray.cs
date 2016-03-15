using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class LargestSumContiguousInArray : Runable, IQuestion<int[], int>
    {
        public ExpectedInputAndOutput<int[], int>[] TestValues {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {1,2,3,4,5,6,7,8,9}, Output = 45},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {-1,-2,-3,-4,-5,-6,-7,-8,-9}, Output = -1},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,8,7,6,5,4,3,2,1}, Output = 45},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,8,7,6,5,4,3,2,1,0,-1}, Output = 45},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9,-10,7,1,0,-1}, Output = 9},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {-1,0,1,2,3}, Output = 6},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {9}, Output = 9},
                    new ExpectedInputAndOutput<int[], int>() {Input = new int[] {-2, 1, -3, 4, -1, 2, 1, -5, 4}, Output = 6}
                };
            }
        }
        public int Run(int[] input)
        {
            if (input.Length == 0)
                throw new ArgumentException("Input cannot have 0 characters.");
            if (input.Length == 1)
                return input[0];

            int sum = int.MinValue;
            int bestSum = int.MinValue;
            for (int i = 0; i < input.Length; i++)
            {
                if(sum < 0)
                    sum = input[i];
                else
                {
                    sum += input[i];
                }

                if (sum > bestSum)
                    bestSum = sum;
            }
            return bestSum;
        }

        public string QuestionName {
            get { return "Largest sum of contiguous integers in an array.";
            }
        }
    }
}
