namespace Patterns
{
    public interface IStep
    {
        void next(IStep nextStep);
        object Transform(object input);

    }

    public interface IStep<TIn, TOut> : IStep
    {
        TOut Transform(TIn input);
    }
}
