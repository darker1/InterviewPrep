using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class BuildBinaryTree : Runable, IQuestion<int, BinaryTreeNode<bool>>
    {
        public ExpectedInputAndOutput<int, BinaryTreeNode<bool>>[] TestValues {
            get
            {
                return new[]
                {
                    new ExpectedInputAndOutput<int, BinaryTreeNode<bool>>() {Input = 0, Output = new BinaryTreeNode<bool>()},
                    new ExpectedInputAndOutput<int, BinaryTreeNode<bool>>() {Input = 1, Output = new BinaryTreeNode<bool>() {Left = new BinaryTreeNode<bool>(), Right = new BinaryTreeNode<bool>()} },
                    new ExpectedInputAndOutput<int, BinaryTreeNode<bool>>() {Input = 2, Output = new BinaryTreeNode<bool>() {
                        Left = new BinaryTreeNode<bool>() { Left = new BinaryTreeNode<bool>(), Right = new BinaryTreeNode<bool>()},
                        Right = new BinaryTreeNode<bool>() { Left = new BinaryTreeNode<bool>(), Right = new BinaryTreeNode<bool>()}} },
                };
            }
        }

        public BinaryTreeNode<bool> Run(int input)
        {
            var node = new BinaryTreeNode<bool>();
            if (input == -1)
                return null;

            node.Left = Run(input - 1);
            node.Right = Run(input - 1);

            return node;
        }

        public string QuestionName {
            get { return "Build a binary tree of height N.  Each node shall contain a bool with False as Data."; }
        }
    }
}
