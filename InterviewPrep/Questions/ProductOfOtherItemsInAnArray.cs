using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class ProductOfOtherItemsInAnArray : Runable, IQuestion<int[], ComparableArray<long>>
    {
        public ExpectedInputAndOutput<int[], ComparableArray<long>>[] TestValues {
            get { return new ExpectedInputAndOutput<int[], ComparableArray<long>>[]
            {
                new ExpectedInputAndOutput<int[], ComparableArray<long>>() { Input = new []{2,2,2,2,2,2,2}, Output = new ComparableArray<long>() {Array = new long[]{64,64,64,64,64,64,64}} },
                new ExpectedInputAndOutput<int[], ComparableArray<long>>() { Input = new []{2,2,2,3}, Output = new ComparableArray<long>() {Array = new long[]{12,12,12,8}} },
                new ExpectedInputAndOutput<int[], ComparableArray<long>>() { Input = new []{2}, Output = new ComparableArray<long>() {Array = new long[]{0}} },
                new ExpectedInputAndOutput<int[], ComparableArray<long>>() { Input = new []{2,3}, Output = new ComparableArray<long>() {Array = new long[]{3,2}} },
            }; }
        }

        public ComparableArray<long> Run(int[] input)
        {
            if(input.Length == 1)
                return new ComparableArray<long>() {Array = new long[] {0}};

            int length = input.Length;

            long[] rightIncProduct = new long[length];
            long[] leftIncProduct = new long[length];
            long[] output = new long[length];

            rightIncProduct[0] = input[0];
            leftIncProduct[length - 1] = input[length - 1];

            for (int i = 1; i < length; i++)
            {
                rightIncProduct[i] = rightIncProduct[i - 1] * input[i];
                leftIncProduct[length - 1 - i] = leftIncProduct[length - i] * input[length - 1 - i];
            }

            output[0] = leftIncProduct[1];
            output[length - 1] = rightIncProduct[length - 2];

            for (int i = 1; i < length - 1; i++)
            {
                output[i] = leftIncProduct[i + 1] * rightIncProduct[i - 1];
            }

            return new ComparableArray<long>() {Array = output};
        }

        public string QuestionName {
            get { return "Given an array of numbers, replace each number with the product of all the numbers in the array except the number itself *without* using division"; }
        }

    }
}
