namespace BlackDigital.Rest.Transforms
{
    public interface ITransformRule
    {
        object? Transform(object? value);

        Task<object?> TransformAsync(object? value);
    }

    public interface ITransformRule<T> : ITransformRule
    {
        T? Transform(T? value);
        Task<T?> TransformAsync(T? value);
    }

    public interface ITransformRule<TIn, TOut> : ITransformRule
    {
        TOut? Transform(TIn? value);
        Task<TOut?> TransformAsync(TIn? value);
    }
}
