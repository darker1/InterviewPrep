using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    /// <summary>
    /// Use this attribute to signal to only run each case a certain number of times
    /// By default the system runs each test 1000 times, but with N^2 and slower algorithms, 
    /// sometimes this is unacceptably slow.  Use this attribute to reduce the number of runs 
    /// per attempt. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RunXTimesAttribute : Attribute
    {
        public int Times { get; }

        public RunXTimesAttribute(int times)
        {
            Times = times;
        }
    }
}
