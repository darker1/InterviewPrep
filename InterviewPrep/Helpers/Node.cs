namespace InterviewPrep.Helpers
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }


        public Node(T data)
        {
            Next = null;
            Prev = null;
            Data = data;
        }
    }
}
