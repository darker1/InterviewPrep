using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    public class TrieNode<T>
    {
        public Dictionary<T, TrieNode<T>> Children { get; set; }
        public T Data { get; set; }
        public bool IsValid { get; set; }
        public TrieNode()
        {
            Children = new Dictionary<T, TrieNode<T>>();
        }
    }
}
