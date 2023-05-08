namespace BlackDigital.Model
{
    public interface IId
    {
        Id Id { get; }
    }

    public interface IId<TKey>
        where TKey : struct
    {
        TKey Id { get; }
    }


    public interface IdByte : IId<byte> { }

    public interface IdSByte : IId<sbyte> { }

    public interface IdShort : IId<short> { }

    public interface IdUShort : IId<ushort> { }

    public interface IdInt : IId<int> { }

    public interface IdUInt : IId<uint> { }

    public interface IdLong : IId<long> { }

    public interface IdULong : IId<ulong> { }

    public interface IdGuid : IId<Guid> { }
}
