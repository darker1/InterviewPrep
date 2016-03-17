using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class MergeSort : Runable, IQuestion<int[], ComparableArray<int>>
    {
        private List<ExpectedInputAndOutput<int[], ComparableArray<int>>> _expectedInputsAndOutputs;

        public ExpectedInputAndOutput<int[], ComparableArray<int>>[] TestValues {
            get { return _expectedInputsAndOutputs.ToArray(); }
        }
        public ComparableArray<int> Run(int[] input)
        {
            if (input == null || input.Length == 0)
                return null;

            //ok lets sort
            Split(input, 0,input.Length, new int[input.Length]);

            return new ComparableArray<int>() {Array = input};
        }

        private void Split(int[] arr, int begin, int end, int[] workArray)
        {
            //stop when size is 1
            if (end - begin < 2)
                return;

            int middle = (begin + end)/2;

            Split(arr, begin, middle, workArray); // split the left into single chunks
            Split(arr, middle, end, workArray);   // split the right into single chunks
            Merge(arr, begin, middle, end, workArray);
            Copy(arr, begin, end, workArray);
        }

        private void Merge(int[] arr, int begin, int mid, int end, int[] workArr)
        {
            int leftIndex = begin;
            int rightIndex = mid;
            for (int i = begin; i < end; i++)
            {
                if (leftIndex < mid &&(rightIndex >= end || arr[leftIndex] <= arr[rightIndex]))
                {
                    //if left is smaller use it
                    workArr[i] = arr[leftIndex++];
                }
                else
                {
                    //if right is smaller use it
                    workArr[i] = arr[rightIndex++];
                }
            }
        }

        private void Copy(int[] arr, int begin, int end, int[] workArr)
        {
            for (int i = begin; i < end; i++)
                arr[i] = workArr[i];
        }


        public string QuestionName
        {
            get
            {
                return "Implement Merge Sort";
            }
        }

        public MergeSort()
        {
            _expectedInputsAndOutputs = new List<ExpectedInputAndOutput<int[], ComparableArray<int>>>(3);
            Random rand = new Random();
            for (int i = 1; i <= 3; i++)
            {
                int numItems = rand.Next(5,(int)Math.Pow(10, i));
                int[] input = new int[numItems]; 
                for (int j = 0; j < numItems; j++)
                {
                    input[j] = rand.Next(numItems * 100);
                }
                _expectedInputsAndOutputs.Add(new ExpectedInputAndOutput<int[], ComparableArray<int>>() {Input = input, Output = new ComparableArray<int>() {Array = input.OrderBy(x => x).ToArray()} });
            }
        }
    }
}
