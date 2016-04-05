using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Dictionary;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class Boggle : Runable, IQuestion<char[][], ComparableArray<string>>
    {
        public ExpectedInputAndOutput<char[][], ComparableArray<string>>[] TestValues
        {
            get
            {
                return new ExpectedInputAndOutput<char[][], ComparableArray<string>>[]
                {
                    new ExpectedInputAndOutput<char[][], ComparableArray<string>>()
                    {
                        Input = new char[][] {new char[] {'a', 'd'}, new char[] {'n', 'o'}},
                        Output =
                            new ComparableArray<string>()
                            {
                                Array = new[] {"a", "ado", "an", "and", "dan", "do", "don", "no", "nod", "on"}
                            }
                    },
                    new ExpectedInputAndOutput<char[][], ComparableArray<string>>()
                    {
                        Input =
                            new char[][]
                            {
                                new char[] {'a', 'h', 'c', 'e'}, new char[] {'e', 'w', 's', 'l'},
                                new char[] {'f', 'x', 's', 'i'}, new char[] {'z', 't', 's', 'r'}
                            },
                        Output = new ComparableArray<string>()
                        {
                            Array = new[]
                            {
                                "a", "ah", "awe", "ha", "he", "hew", "heft", "hex", "chew"
                                , "chews", "chef", "cess", "eschew", "eschews", "els"
                                , "eh", "ext", "we", "shaw", "she", "schwa", "sec", "sis"
                                , "sir", "sirs", "less", "list", "lists", "few", "i"
                                , "is", "isle", "isles", "rise", "rile", "riles"
                            }
                        }
                    },
                };
            }
        }

        public ComparableArray<string> Run(char[][] input)
        {
            if (input == null)
                return new ComparableArray<string>() {Array = Enumerable.Empty<string>().ToArray()};

            int xl = input.Length;
            int yl = input[0].Length;

            TrieNode<char> dictionary = LowercaseWords.TrieDictionary;
            HashSet<string> builtStrings = new HashSet<string>();

            for (int x = 0; x < xl; x++)
            {
                for (int y = 0; y < yl; y++)
                {
                    SearchForWords(dictionary, input, builtStrings,
                        new HashSet<Tuple<int, int>>(), x, y, string.Empty);

                }
            }

            return new ComparableArray<string>() {Array = builtStrings.ToArray()};
        }

        private void SearchForWords(TrieNode<char> dictionary, char[][] arr, HashSet<string> builtStrings,
            HashSet<Tuple<int, int>> visitedIndexes, int cX, int cY, string currentString)
        {
            //make sure not visited
            var tmp = new Tuple<int, int>(cX, cY);
            if (visitedIndexes.Contains(tmp))
            {
                return;
            }
            //visit
            visitedIndexes.Add(tmp);
            //update the string
            char c = arr[cX][cY];
            currentString += c;

            //see if that could be a word
            if (dictionary.Children.ContainsKey(c))
            {
                var currentNode = dictionary.Children[c];
                if (currentNode.IsValid)
                {
                    if (!builtStrings.Contains(currentString))
                        builtStrings.Add(currentString);
                }

                //check all neighboring locations
                int xl = arr.Length;
                int yl = arr[0].Length;
                for (int x = cX - 1; x <= cX + 1; x++)
                {
                    for (int y = cY - 1; y <= cY + 1; y++)
                    {
                        if (x >= 0 && x < xl && y >= 0 && y < yl)
                        {
                            //if Neighboring location is good...recurse
                            SearchForWords(currentNode, arr, builtStrings, visitedIndexes, x, y, currentString);
                        }
                    }
                }
            }
            //clean up string and visited
            visitedIndexes.Remove(tmp);
        }

    public string QuestionName {
            get { return "Given a matrix of size x, return all the possible words in the boggle.  Remember a word can be connected diagonally, horizontally, or vertically."; }
        }
    }
}
