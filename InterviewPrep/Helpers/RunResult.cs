using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    public class RunResult
    {
        public string QuestionName { get; set; }
        public Exception Exception { get; set; }
        public int Run { get; set; }
        public dynamic Input { get; set; }
        public string InputString {
            get { return GenerateInputString(Input); }
        }
        public dynamic Output { get; set; }
        public dynamic ExpectedOutput { get; set; }
        public long AverageTicksPerAttempt { get; set; }
        public Outcome Outcome { get; set; }

        public string GetConsoleString()
        {
            return string.Format(
               "{0}! Run: {1}, Input: {2}, Output: {3} {4} Average Run Attempt: {5} ticks"
               , Outcome, Run, InputString, Output, Environment.NewLine,AverageTicksPerAttempt);
        }

        private static string GenerateInputString(dynamic input)
        {
            StringBuilder inputString = null;
            if (input is IEnumerable && !(input is string))
            {
                var enumerator = input.GetEnumerator() as IEnumerator;

                inputString = new StringBuilder("[");
                bool tooLong = false;
                object last = null;
                while (enumerator.MoveNext())
                {
                    if (inputString.Length + enumerator.Current.ToString().Length > 50)
                    {
                        tooLong = true;
                    }

                    if (tooLong)
                    {
                        last = enumerator.Current;
                        continue;
                    }

                    inputString.AppendFormat(" {0},", enumerator.Current);
                }
                if (tooLong) //either add the ... and final value
                    inputString.AppendFormat("... {0}", last);
                else // or get rid of the trailing comma
                    inputString.Remove(inputString.Length - 1, 1);
                inputString.Append("]");
            }
            if (inputString == null)
            {
                string s = input.ToString();
                if (s.Length > 55)
                {
                    return string.Format("{0} ... {1}", s.Substring(0, 50), s.Substring(s.Length - 4));
                }
                return s;
            }

            return inputString.ToString();
        }
    }
}
