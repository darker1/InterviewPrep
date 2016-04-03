using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    class EnumerableOfEnumerable : Runable, IQuestion<IEnumerable<IEnumerable<char>>, string>
    {
        public ExpectedInputAndOutput<IEnumerable<IEnumerable<char>>, string>[] TestValues {
            get { return new ExpectedInputAndOutput<IEnumerable<IEnumerable<char>>, string>[]
            {
                new ExpectedInputAndOutput<IEnumerable<IEnumerable<char>>, string>() {Input = new List<IEnumerable<char>>()
                {
                    new []{ 'a', 'e', 'i', 'k'},
                    new []{ 'b', 'f', 'j'},
                    new []{ 'c', 'g'},
                    new []{ 'd', 'h'},
                }, Output = "abcdefghijk"},
                new ExpectedInputAndOutput<IEnumerable<IEnumerable<char>>, string>() {Input = new List<IEnumerable<char>>()
                {
                    new []{ '1', '5', '9'},
                    new []{ '2', '6', '0', 'b'},
                    new []{ '3', '7'},
                    new []{ '4', '8', 'a'},
                }, Output = "1234567890ab"}
            };}
        }
        public string Run(IEnumerable<IEnumerable<char>> input)
        {
            CrazyEnumerator e = new CrazyEnumerator(input);
            StringBuilder builder = new StringBuilder();

            while (e.MoveNext())
            {
                builder.Append(e.Current);
            }

            return builder.ToString();
        }

        private class CrazyEnumerator : IEnumerator<char>
        {
            private readonly IEnumerable<IEnumerable<char>> _collection;
            private IEnumerator<IEnumerable<char>> _outerIterator;
            private int _depth;
            private bool _reset = false;


            public CrazyEnumerator(IEnumerable<IEnumerable<char>> collection)
            {
                _collection = collection;
                _outerIterator = collection.GetEnumerator();
                _depth = 1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_outerIterator.MoveNext())
                {
                    var innerIterator = _outerIterator.Current.GetEnumerator();
                    for (int i = 0; i < _depth; i++)
                    {
                        if (!innerIterator.MoveNext())
                        {
                            //go to the next outter
                            return MoveNext();
                        }
                    }

                    _reset = false;

                    Current = innerIterator.Current;
                    return true;
                }
                else
                {
                    if (_reset)
                        return false;

                    Reset();
                    _depth++;
                    return MoveNext();
                }
            }
            
            public void Reset()
            {
                _reset = true;
                _outerIterator = _collection.GetEnumerator();
            }

            public char Current { get; private set; }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        public string QuestionName {
            get { return "Create a crazy enumerator class that takes an IEnumerable<IEnumerable<char>> and traverses it by visiting each Outer IEnumerable once before moving down the line on the interior IEnumerables.  Ex. [[a,d],[b,e],[c]] would print out in the order abcde."; }
        }
    }
}
