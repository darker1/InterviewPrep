using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace InterviewPrep.Helpers
{
    public abstract class Runable : IRunable
    {
        private const int RunXTimes = 1000;

        private object Round(decimal value, int numberOfPlaces)
        {
            return decimal.Round(value, numberOfPlaces);
        }

        private object Round(Double value, int numberOfPlaces)
        {
            return Math.Round(value, numberOfPlaces);
        }

        private int GetTimesToRunEachTest()
        {
            var attributes = this.GetType().GetCustomAttributes(true);
            var runXTimesAttribute = attributes.SingleOrDefault(a => a is RunXTimesAttribute) as RunXTimesAttribute;
            return runXTimesAttribute?.Times ?? RunXTimes;
        }

        public IEnumerable<string> Run()
        {
            int count = 0;
            var results = new List<string>();

            var timesToRun = GetTimesToRunEachTest();

            dynamic obj = this;
            var genericTypes = this.GetType().GetInterfaces().First(i => i.IsGenericType).GetGenericArguments();

            var inputType = genericTypes[0];
            var outputType = genericTypes[1];

            var enumerator = obj.TestValues.GetEnumerator() as IEnumerator;
            results.Add(obj.QuestionName as string);
            while (enumerator.MoveNext())
            {
                count++;
                dynamic inputAndOutput = enumerator.Current;

                var input = inputAndOutput.Input;
                var expectedOutput = inputAndOutput.Output;

                Func<object, object> roundFunction = inputAndOutput.ShouldRoundOutput ? 
                    (o) =>
                    {
                        if (inputType == typeof (decimal))
                            return Round((decimal) o, (int) (inputAndOutput.RoundDecimalPlaces));
                        return Round((decimal) o, (int) (inputAndOutput.RoundDecimalPlaces));
                    } : 
                    (Func<object, object>) (o => o); //have to cast to make the compiler happy 

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    var output = roundFunction(obj.Run(input));

                    if ((output as IComparable).CompareTo(expectedOutput) != 0)
                        results.Add(
                            string.Format(
                                "Run: {0}, Input: {1}, Expected Output: {2}, Actual Output: {3}, Failed after {4} ticks."
                                , count, input, expectedOutput, output, stopwatch.ElapsedTicks));
                    else
                    {

                        int ticksAllowedPerAttempt = inputAndOutput.TicksAllowedPerAttempt;
                        int completedInTime = 0;
                        var stopwatch2 = new Stopwatch();
                        stopwatch2.Start();
                        for (int i = 0; i < timesToRun; i++)
                        {
                            obj.Run(input);
                            stopwatch2.Stop();
                            if (ticksAllowedPerAttempt > stopwatch2.ElapsedTicks / timesToRun)
                                completedInTime++;
                            stopwatch2.Start();
                        }
                        stopwatch2.Stop();
                        results.Add(
                            ReportTestRun(Outcome.Passed, count, input, output, stopwatch2.ElapsedTicks, timesToRun, completedInTime));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(
                        string.Format(
                            "Run: {0}, Input: {1}, Expected Output: {2}, Failed after {3} ticks. EXCEPTION: {4}"
                            , count, input, expectedOutput, stopwatch.ElapsedTicks, ex.ToString()));
                }
                stopwatch.Stop();
            }

            return results;
        }

        private static string ReportTestRun(Outcome outcome, int count, dynamic input, dynamic output, long ticks, int timesToRun, int completedInTime)
        {
            
            return string.Format(
                "{0}! Run: {1}, Input: {2}, Output: {3} {4} Average Run Attempt: {5} ticks"
                , outcome, count, GenerateInputString(input), output, Environment.NewLine,(int) (ticks/timesToRun));
        }

        private enum Outcome
        {
            Passed, 
            Failed, 
            Exception
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