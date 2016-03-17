InterviewPrep
========
This is a repo of different interview questions and some answers.  Please note that these are not the best answers out there, they are simply the best I could come up with during timeboxed coding sessions.

Add a sample Question
========
Creating a sample question has 3 requirements.  
Namespace

    namespace InterviewPrep.Questions

Inheritance

    Runable

Extend Interface

    interface IQuestion<TInput, TOutput> where TOutput : IComparable
  

Expectations of the IQuestion Interface
========

    public interface IQuestion<TInput, TOutput> where TOutput : IComparable
    {
        ExpectedInputAndOutput<TInput, TOutput>[] TestValues { get; }
        TOutput Run(TInput input);
        string QuestionName { get; }
    }

if TInput is IClonable, the runner will give the question a deep copy of the input as to preserve the original input. 

Test Values
------

    ExpectedInputAndOutput<TInput, TOutput>[] TestValues { get; }

This is expecting an array of all the test cases you wish to use.  I suggest simply initializing it at the top of your question for clarity.  

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
        
Question Name
------
This identifies what the question is actually asking of the user.  It can be as verbose or brief as you desire.  

        public string QuestionName 
        {
            get { return "Find the First Non-Repeating Character in String."; }
        }
        
Run
------
This is where the magic of your answer is stored.  If you are using this to prep for an interview (interviewee or interviewer), I suggest you timebox your typing in this area.  
It is important to note that the input and output of this function are the generic parts of your IQuestion (See above).  

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
        
Running your new Question
======
Simply run the application.  I built a runner into program, so it will pick up your question and run it for you.  
**I think it does not display exceptions correctly right now.  I will fix that soon**
