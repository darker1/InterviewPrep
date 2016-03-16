using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InterviewPrep.Helpers;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new List<RunResult>();
            var toTest = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "InterviewPrep.Questions" && t.IsClass && t.IsSubclassOf(typeof(Runable)))
                .Select(t => new { Runable = Activator.CreateInstance(t) as IRunable, Type = t});

            foreach (var test in toTest)
            {
                output.AddRange(test.Runable.Run());
            }

            foreach (var o in output.GroupBy(x => x.QuestionName))
            {
                Console.WriteLine(o.Key);
                foreach (var result in o)
                {
                    Console.WriteLine(result.GetConsoleString());
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
