using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    public class BinarySearchTree<T> : BinaryTreeNode<T> where T : IComparable
    {
        private bool _inserted = false;

        public void Insert(T val)
        {
            if (!_inserted)
            {
                _inserted = true;
                this.Data = val;
            }

            Insert(this, val);
        }

        private void Insert(BinaryTreeNode<T> node, T val)
        {
            int comparison = val.CompareTo(node.Data);
            if (comparison == 0)
                return;
            if (comparison > 0)
            {
                if (node.Right != null)
                    Insert(node.Right, val);
                else
                    node.Right = new BinaryTreeNode<T>() {Data = val};
            }
            else
            {
                if (node.Left != null)
                    Insert(node.Left, val);
                else
                    node.Left = new BinaryTreeNode<T>() { Data = val };
            }
        }
    }
}
