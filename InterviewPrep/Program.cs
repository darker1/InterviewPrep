using InterviewPrep.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new List<string>();
            var toTest = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "InterviewPrep.Questions" && t.IsClass && t.IsSubclassOf(typeof(Runable)))
                .Select(t => new { Runable = Activator.CreateInstance(t) as IRunable, Type = t});

            foreach (var test in toTest)
            {
                output.AddRange(test.Runable.Run());
                output.Add(" ");
            }

            foreach (var o in output)
            {
                Console.WriteLine(o);
            }

            Console.ReadKey();
        }
    }
}
