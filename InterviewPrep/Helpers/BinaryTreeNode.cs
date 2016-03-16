using System;

namespace InterviewPrep.Helpers
{
    public class BinaryTreeNode<T> : IComparable, IComparable<BinaryTreeNode<T>> where T : IComparable
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as BinaryTreeNode<T>); 
        }

        public int CompareTo(BinaryTreeNode<T> other)
        {
            var data = Data.CompareTo(other.Data);
            if(data != 0)
                return data;

            int left = NodeCheck(Left, other.Left);
            if (left != 0)
                return left;

            int right = NodeCheck(Right, other.Right);
            if (right != 0)
                return right;
            
            return 0;
        }

        private static int NodeCheck(BinaryTreeNode<T> node1, BinaryTreeNode<T> node2)
        {
            if (BothNull(node1, node2))
                return 0;

            if (CanCheckNodes(node1, node2))
                return node1.CompareTo(node2);

            return node1 == null ? -1 : 1;
        }

        private static bool CanCheckNodes(BinaryTreeNode<T> node1, BinaryTreeNode<T> node2)
        {
            return (node1 != null && node2 != null);
        }

        private static bool BothNull(BinaryTreeNode<T> node1, BinaryTreeNode<T> node2)
        {
            return node1 == null && node2 == null;
        }
    }
}