namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having an <see cref="Id"/>.
    /// </summary>
    public interface IId
    {
        /// <summary>
        /// Gets the identifier of the model.
        /// </summary>
        Id Id { get; }
    }

    /// <summary>
    /// Marks a model as having a typed identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of the identifier.</typeparam>
    public interface IId<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Gets the identifier of the model.
        /// </summary>
        TKey Id { get; }
    }


    /// <summary>
    /// Marks a model as having a <see cref="byte"/> identifier.
    /// </summary>
    public interface IdByte : IId<byte> { }

    /// <summary>
    /// Marks a model as having a <see cref="sbyte"/> identifier.
    /// </summary>
    public interface IdSByte : IId<sbyte> { }

    /// <summary>
    /// Marks a model as having a <see cref="short"/> identifier.
    /// </summary>
    public interface IdShort : IId<short> { }

    /// <summary>
    /// Marks a model as having a <see cref="ushort"/> identifier.
    /// </summary>
    public interface IdUShort : IId<ushort> { }

    /// <summary>
    /// Marks a model as having an <see cref="int"/> identifier.
    /// </summary>
    public interface IdInt : IId<int> { }

    /// <summary>
    /// Marks a model as having a <see cref="uint"/> identifier.
    /// </summary>
    public interface IdUInt : IId<uint> { }

    /// <summary>
    /// Marks a model as having a <see cref="long"/> identifier.
    /// </summary>
    public interface IdLong : IId<long> { }

    /// <summary>
    /// Marks a model as having a <see cref="ulong"/> identifier.
    /// </summary>
    public interface IdULong : IId<ulong> { }

    /// <summary>
    /// Marks a model as having a <see cref="Guid"/> identifier.
    /// </summary>
    public interface IdGuid : IId<Guid> { }
}
