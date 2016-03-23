using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class GivenPatternFindAllPossibleStrings : Runable, IQuestion<String, ComparableArray<string>>
    {
        public ExpectedInputAndOutput<string, ComparableArray<string>>[] TestValues {
            get
            {
                return new ExpectedInputAndOutput<string, ComparableArray<string>>[]
                {
                    new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "", Output = new ComparableArray<string>() {Array = new []{""}} },
                    new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "101010", Output = new ComparableArray<string>() {Array = new []{"101010"}} },
                    new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "10?1010", Output = new ComparableArray<string>() {Array = new []{"1001010", "1011010"}} },
                    new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "10?10?10", Output = new ComparableArray<string>() {Array = new []{"10010010", "10010110", "10110010", "10110110" }} },
                };
            }
        }

        public ComparableArray<string> Run(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ComparableArray<string>() {Array = new []{""}};
            }

            IEnumerable<VariableTreeNode> root  = AddNextValue(input, 0);

            List<string> output = new List<string>();
            foreach (var node in root)
            {
                DfsStringGeneration(node, string.Empty, output);
            }
            return new ComparableArray<string>() {Array = output.ToArray()};
        }

        private IEnumerable<VariableTreeNode> AddNextValue(string input, int index)
        {
            if (input.Length == index)
            {
                return null;
            }

            if (input[index] == '?')
            {
                return new[]
                {
                    new VariableTreeNode() {Value = '0', Children = AddNextValue(input, index + 1)},
                    new VariableTreeNode() {Value = '1', Children = AddNextValue(input, index + 1)},
                };
            }

            return new[]
            {
                new VariableTreeNode() {Value = input[index], Children = AddNextValue(input, index + 1)},
            };
        }

        private void DfsStringGeneration(VariableTreeNode current, string s, List<string> builtStrings)
        {
            if (current.Children == null)
            {
                builtStrings.Add(s + current.Value);
                return;
            }

            foreach (var node in current.Children)
            {
                DfsStringGeneration(node, s + current.Value, builtStrings);
            }
        }


        public string QuestionName
        {
            get
            {
                return "Given a string pattern of 0s, 1s, and ?s (wildcards), generate all 0-1 strings that match this pattern.";
            }
        }

        private class VariableTreeNode
        {
            public char Value { get; set; }
            public IEnumerable<VariableTreeNode> Children { get; set; }
        }

    }
}
