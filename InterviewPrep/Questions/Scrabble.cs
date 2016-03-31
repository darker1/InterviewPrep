using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Dictionary;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class Scrabble : Runable, IQuestion<String, ComparableArray<string>>
    {
        public ExpectedInputAndOutput<string, ComparableArray<string>>[] TestValues {
            get { return new ExpectedInputAndOutput<string, ComparableArray<string>>[]
            {
                new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "AD*", Output = new ComparableArray<string>() {Array = new string[] { "a", "ad*", "a*d", "a*", "da*", "d*", "*", "*ad", "*a", "*d" }} },
                new ExpectedInputAndOutput<string, ComparableArray<string>>() {Input = "ABCD", Output = new ComparableArray<string>() {Array = new string[] { "a", "bad", "cab", "cad", "dab" }} }, 
            };}
        }
        public ComparableArray<string> Run(string input)
        {
            TrieNode<char> dictionary = BuildDictionary();

            HashSet<string> output = new HashSet<string>();

            SearchTrie(dictionary, input.ToLowerInvariant().ToList(), new List<char>(input.Length), output);

            return new ComparableArray<string>() {Array = output.ToArray()};
        }

        public void SearchTrie(TrieNode<char> currentNode, List<char> remaining, List<char> currentString, HashSet<string> output)
        {
            if (remaining.Count == 0)
                return;

            int toRun = remaining.Count;
            for (int i = 0; i < toRun; i++)
            {
                char currentChar = remaining[i];

                remaining.RemoveAt(i);
                currentString.Add(currentChar);

                if (currentChar != '*')
                {
                    if (currentNode.Children.ContainsKey(currentChar))
                    {
                        if (currentNode.Children[currentChar].IsValid) 
                        output.Add(new string(currentString.ToArray()));

                        SearchTrie(currentNode.Children[currentChar], remaining, currentString, output);
                    }
                }
                else
                {
                    foreach (var child in currentNode.Children)
                    {                     
                        if (child.Value.IsValid)
                            output.Add(new string(currentString.ToArray()));

                        SearchTrie(child.Value, remaining, currentString, output);
                    }
                }

                //reset items
                remaining.Insert(i, currentChar);
                currentString.RemoveAt(currentString.Count - 1);
            }

        }

        private TrieNode<char> BuildDictionary()
        {
            return LowercaseWords.TrieDictionary;
        }

        public string QuestionName {
            get { return "Given alpha characters and a wild card '*', compute all possible words"; }
        }
    }
}
