using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class FibinacciNumbersLessThanN : Runable, IQuestion<int, int>
    {
        public ExpectedInputAndOutput<int, int>[] TestValues {
            get { return new[]
            {
                new ExpectedInputAndOutput<int, int>() {Input = 6, Output = 6},
                new ExpectedInputAndOutput<int, int>() {Input = 1, Output = 1},
                new ExpectedInputAndOutput<int, int>() {Input = 2, Output = 3},
                new ExpectedInputAndOutput<int, int>() {Input = 36, Output = 10},

            }; }
        }
        public int Run(int input)
        {
            if (input <= 0)
                return 0;
            if (input == 1)
                return 1;
            if (input == 2)
                return 3;

            int countOfNumbers = 3;
            int i = 1;
            int j = 1;

            while (input > i)
            {
                int next = i + j;
                j = i;
                i = next;
                countOfNumbers++;
            }
            return countOfNumbers - 1;
        }

        public string QuestionName {
            get
            {
                return "How many Fibonacci numbers exists less than a given number n.Can you find a function in terms of n , to get the number of fibonacci number less than n.";
            }
        }
    }
}
