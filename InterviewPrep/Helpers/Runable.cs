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

        public IEnumerable<RunResult> Run()
        {
            int count = 0;
            var results = new List<RunResult>();

            var timesToRun = GetTimesToRunEachTest();

            dynamic obj = this;
            var genericTypes = this.GetType().GetInterfaces().First(i => i.IsGenericType).GetGenericArguments();

            var inputType = genericTypes[0];
            var outputType = genericTypes[1];

            var enumerator = obj.TestValues.GetEnumerator() as IEnumerator;
            string questionName = (obj.QuestionName as string);

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
                        results.Add(ReportTestRun(Outcome.Failed, count, input,output,stopwatch.ElapsedTicks,1,questionName));
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
                            ReportTestRun(Outcome.Passed, count, input, output, stopwatch2.ElapsedTicks, timesToRun, questionName));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(ReportTestRun(Outcome.Exception, count,input, expectedOutput,stopwatch.ElapsedTicks, 1,questionName,ex ));
                }
                stopwatch.Stop();
            }

            return results;
        }

        private static RunResult ReportTestRun(Outcome outcome, int count, dynamic input, dynamic output, long ticks, int timesToRun, string questionName, Exception exception = null)
        {
            return new RunResult()
            {
                Outcome = outcome,
                AverageTicksPerAttempt = (int)(ticks / timesToRun),
                Exception = exception, 
                Input = input, 
                Output = output,
                Run = count, 
                QuestionName = questionName
            };
        }
    }
}