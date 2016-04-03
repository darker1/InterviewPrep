using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    class Anagrams : Runable, IQuestion<Tuple<string, string>, bool>
    {
        public ExpectedInputAndOutput<Tuple<string, string>, bool>[] TestValues {
            get { return new ExpectedInputAndOutput<Tuple<string, string>, bool>[]
            {
                new ExpectedInputAndOutput<Tuple<string, string>, bool>() {Input = new Tuple<string, string>("Doctor Who","Torchwood"), Output = true},
                new ExpectedInputAndOutput<Tuple<string, string>, bool>() {Input = new Tuple<string, string>("Doctor Whos","Torchwood"), Output = false},
            };
            }
        }
        public bool Run(Tuple<string, string> input)
        {
            string s1 = input.Item1;
            string s2 = input.Item2;

            int s1L = s1.Length;
            int s2L = s2.Length;
            int maxLen = Math.Max(s1L, s2L);
            
            Dictionary<char, int[]> characterCounts = new Dictionary<char, int[]>(26);

            for (int i = 0; i < maxLen; i++)
            {
                if (s1L - 1 >= i)
                {
                    HandleCharacter( s1[i], 0, characterCounts); 
                }
                if (s2L - 1 >= i)
                {
                    HandleCharacter(s2[i], 1, characterCounts);
                }
            }
            foreach (var arr in characterCounts.Values)
            {
                if (arr[0] != arr[1])
                    return false;
            }
            return true;
        }

        private void HandleCharacter(char c, int i, Dictionary<char, int[]> dict)
        {
            c = char.ToLowerInvariant(c);
            if (!char.IsWhiteSpace(c))
            {
                if (!dict.ContainsKey(c))
                    dict.Add(c, new[] { i == 0 ? 1:0, i == 1 ? 1 : 0 });
                else
                    dict[c][i]++;
            }
        }

        public string QuestionName {
            get { return "Find out if two string words are anagrams. Doctor Who and Torchwood are an example."; }
        }
    }
}
