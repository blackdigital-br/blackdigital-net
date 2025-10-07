
namespace BlackDigital.Rest.Transforms
{
    public class TransformRule : ITransformRule
    {
        public virtual object? Transform(object? value)
        {
            return value;
        }

        public virtual Task<object?> TransformAsync(object? value)
        {
            return Task.FromResult(Transform(value));
        }
    }

    public class TransformRule<T> : ITransformRule<T>
    {
        public virtual T? Transform(T? value)
        {
            return value;
        }

        public virtual Task<T?> TransformAsync(T? value)
        {
            return Task.FromResult(Transform(value));
        }

        object? ITransformRule.Transform(object? value)
            => Transform((T?)value);

        Task<object?> ITransformRule.TransformAsync(object? value)
            => Task.FromResult((object?)Transform((T?)value));
    }

    public class TransformRule<TIn, TOut> : ITransformRule<TIn, TOut>
    {
        public virtual TOut? Transform(TIn? value)
        {
            return default;
        }

        public virtual Task<TOut?> TransformAsync(TIn? value)
        {
            return Task.FromResult(Transform(value));
        }

        object? ITransformRule.Transform(object? value)
            => Transform((TIn?)value);

        Task<object?> ITransformRule.TransformAsync(object? value)
            => Task.FromResult((object?)Transform((TIn?)value));
    }
}
