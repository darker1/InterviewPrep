using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    public class TreeNode<T> 
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
    }
}
