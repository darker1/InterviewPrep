using System;

namespace InterviewPrep.Helpers
{
    public interface IQuestion<TInput, TOutput> where TOutput : IComparable
    {
        ExpectedInputAndOutput<TInput, TOutput>[] TestValues { get; }
        TOutput Run(TInput input);

        string QuestionName { get; }
    }
}
