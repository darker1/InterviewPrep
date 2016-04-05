using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class SecondLargestItemInBst : Runable, IQuestion<BinarySearchTree<int>, int>
    {
        private ExpectedInputAndOutput<BinarySearchTree<int>, int>[] _input;

        public SecondLargestItemInBst()
        {
            _input = new ExpectedInputAndOutput<BinarySearchTree<int>, int>[2];
            _input[0] = new ExpectedInputAndOutput<BinarySearchTree<int>, int>() {Input = new BinarySearchTree<int>(), Output = 9};
            foreach (var i in (new int[] { 10, 8, 9, 6, 5, 2, 3, 4 }))
            {
                 _input[0].Input.Insert(i);
            }

            _input[1] = new ExpectedInputAndOutput<BinarySearchTree<int>, int>() { Input = new BinarySearchTree<int>(), Output = 9 };
            foreach (var i in (new int[] { 5, 8, 9, 6, 2, 3, 4, 10, 1 }))
            {
                _input[1].Input.Insert(i);
            }

        }

        public ExpectedInputAndOutput<BinarySearchTree<int>, int>[] TestValues {
            get { return _input; }
        }
        public int Run(BinarySearchTree<int> input)
        {
            if(input.Right == null)
                if (input.Left != null)

                    if (input.Left.Right == null)
                        return input.Left.Data;
                    else
                        return DepthFirstSearchReturnLargest(input.Left);

                else
                    throw new Exception("Only one value in tree.");

            return DepthFirstSearchReturnSecond(input);
        }

        public int DepthFirstSearchReturnSecond(BinaryTreeNode<int> current)
        {
            if (current.Right.Right != null)
                return DepthFirstSearchReturnSecond(current.Right);
            return current.Data;
        }

        public int DepthFirstSearchReturnLargest(BinaryTreeNode<int> current)
        {
            if (current.Right != null)
                return DepthFirstSearchReturnLargest(current.Right);
            return current.Data;
        }
        public string QuestionName {
            get { return "Return the second largest character in a Binary Search Tree.  Only traverse the tree once. Do it in O(1) space."; }
        }
    }
}
