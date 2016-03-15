using System;
using System.Collections.Generic;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class FirstNonRepeatingCharacterInString : Runable, IQuestion<string, char>
    {
        public ExpectedInputAndOutput<string, char>[] TestValues
        {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<string, char> { Input = "ABCD", Output = 'A'},
                    new ExpectedInputAndOutput<string, char> { Input = "A", Output = 'A'},
                    new ExpectedInputAndOutput<string, char> { Input = "AaBBbbcCaCAcD", Output = 'D'},
                    new ExpectedInputAndOutput<string, char> { Input = "AaBBbbcCa", Output = 'A'},
                    new ExpectedInputAndOutput<string, char> { Input = "AAAAAAAAAAAAAAAAAAAAAAAAA", Output = ' '},
                };
            }
        }
            
        public char Run(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("Cannot process an empty string", "s");

            if (s.Length == 1)
                return s[0];

            Dictionary<char, Node<char>> characterMap = new Dictionary<char, Node<char>>();
            Node<char> linkedListHead = null;
            Node<char> linkedListTail = null;

            for (int i = 0; i < s.Length; i++)
            {
                //if it does not contain the key
                if (!characterMap.ContainsKey(s[i]))
                {
                    //add to tail, if head is empty... also put at head
                    Node<char> node = new Node<char>(s[i]);

                    if (linkedListHead == null)
                    {
                        linkedListHead = node;
                    }

                    if (linkedListTail != null)
                    {
                        node.Prev = linkedListTail;
                        linkedListTail.Next = node;
                    }

                    linkedListTail = node;

                    characterMap.Add(s[i], node);
                }
                //if it does contain the character and its not null
                else if (characterMap[s[i]] != null)
                {
                    //remove it from the list and set dict.key to null
                    Node<char> node = characterMap[s[i]];

                    //only node in the list -- remove it 
                    if (node.Prev == null && node.Next == null)
                    {
                        linkedListHead = null;
                        linkedListTail = null;
                    }
                    else
                    {
                        if (node.Prev != null)
                            node.Prev.Next = node.Next;

                        if (node.Next != null)
                            node.Next.Prev = node.Prev;
                    }
                    characterMap[s[i]] = null;
                }
                //if it is in the list and null... do nothing
            }

            return linkedListHead != null ? linkedListHead.Data : ' ';
        }

        public string QuestionName {
            get { return "First Non-Repeating Character in String."; }
        }

    }
}