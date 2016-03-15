using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    interface IRunable
    {
        IEnumerable<string> Run();
    }
}
