

using System.Collections.ObjectModel;

namespace BlackDigital.Model
{
    /// <summary>
    /// Represents a strongly-typed list of <see cref="Id"/> values with implicit conversions from and to various collection and array types.
    /// </summary>
    public class ListId : List<Id>
    {
        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class with an empty list.
        /// </summary>
        public ListId()
            : base(new List<Id>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="Id"/> values.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        public ListId(IEnumerable<Id> ids)
            : base(ids.ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of strings.
        /// </summary>
        /// <param name="ids">The collection of identifier strings.</param>
        public ListId(IEnumerable<string> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="Guid"/> values.
        /// </summary>
        /// <param name="ids">The collection of GUID identifiers.</param>
        public ListId(IEnumerable<Guid> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="short"/> values.
        /// </summary>
        /// <param name="ids">The collection of short identifiers.</param>
        public ListId(IEnumerable<short> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="ushort"/> values.
        /// </summary>
        /// <param name="ids">The collection of unsigned short identifiers.</param>
        public ListId(IEnumerable<ushort> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="int"/> values.
        /// </summary>
        /// <param name="ids">The collection of integer identifiers.</param>
        public ListId(IEnumerable<int> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="uint"/> values.
        /// </summary>
        /// <param name="ids">The collection of unsigned integer identifiers.</param>
        public ListId(IEnumerable<uint> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="long"/> values.
        /// </summary>
        /// <param name="ids">The collection of long identifiers.</param>
        public ListId(IEnumerable<long> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListId"/> class from a collection of <see cref="ulong"/> values.
        /// </summary>
        /// <param name="ids">The collection of unsigned long identifiers.</param>
        public ListId(IEnumerable<ulong> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        #endregion "Constructors"


        #region "Operators To"

        /// <summary>
        /// Converts an array of <see cref="Id"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Id[] ids) => new(ids);
        /// <summary>
        /// Converts an array of strings to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(string[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="Guid"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Guid[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="short"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(short[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="ushort"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(ushort[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="int"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(int[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="uint"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(uint[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="long"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(long[] ids) => new(ids);
        /// <summary>
        /// Converts an array of <see cref="ulong"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(ulong[] ids) => new(ids);

        /// <summary>
        /// Converts a list of strings to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<string> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="Guid"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<Guid> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="short"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<short> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="ushort"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<ushort> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="int"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<int> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="uint"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<uint> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="long"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<long> ids) => new(ids);
        /// <summary>
        /// Converts a list of <see cref="ulong"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(List<ulong> ids) => new(ids);

        /// <summary>
        /// Converts a collection of <see cref="Id"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<Id> ids) => new(ids);
        /// <summary>
        /// Converts a collection of strings to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<string> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="Guid"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<Guid> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="short"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<short> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="ushort"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<ushort> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="int"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<int> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="uint"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<uint> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="long"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<long> ids) => new(ids);
        /// <summary>
        /// Converts a collection of <see cref="ulong"/> values to a <see cref="ListId"/>.
        /// </summary>
        public static implicit operator ListId(Collection<ulong> ids) => new(ids);

        #endregion "Operators To"

        #region "Operators From"

        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="Id"/> values.
        /// </summary>
        public static implicit operator Id[](ListId ids) => ids.ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of strings.
        /// </summary>
        public static implicit operator string[](ListId ids) => ids.Select(id => id.ToString()).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="Guid"/> values.
        /// </summary>
        public static implicit operator Guid[](ListId ids) => ids.Select(id => id.ToGuid()).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="short"/> values.
        /// </summary>
        public static implicit operator short[](ListId ids) => ids.Select(id => (short)id).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="ushort"/> values.
        /// </summary>
        public static implicit operator ushort[](ListId ids) => ids.Select(id => (ushort)id).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="int"/> values.
        /// </summary>
        public static implicit operator int[](ListId ids) => ids.Select(id => (int)id).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="uint"/> values.
        /// </summary>
        public static implicit operator uint[](ListId ids) => ids.Select(id => (uint)id).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="long"/> values.
        /// </summary>
        public static implicit operator long[](ListId ids) => ids.Select(id => (long)id).ToArray();
        /// <summary>
        /// Converts a <see cref="ListId"/> to an array of <see cref="ulong"/> values.
        /// </summary>
        public static implicit operator ulong[](ListId ids) => ids.Select(id => (ulong)id).ToArray();

        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of strings.
        /// </summary>
        public static implicit operator List<string>(ListId ids) => ids.Select(id => id.ToString()).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="Guid"/> values.
        /// </summary>
        public static implicit operator List<Guid>(ListId ids) => ids.Select(id => id.ToGuid()).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="short"/> values.
        /// </summary>
        public static implicit operator List<short>(ListId ids) => ids.Select(id => (short)id).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="ushort"/> values.
        /// </summary>
        public static implicit operator List<ushort>(ListId ids) => ids.Select(id => (ushort)id).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="int"/> values.
        /// </summary>
        public static implicit operator List<int>(ListId ids) => ids.Select(id => (int)id).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="uint"/> values.
        /// </summary>
        public static implicit operator List<uint>(ListId ids) => ids.Select(id => (uint)id).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="long"/> values.
        /// </summary>
        public static implicit operator List<long>(ListId ids) => ids.Select(id => (long)id).ToList();
        /// <summary>
        /// Converts a <see cref="ListId"/> to a list of <see cref="ulong"/> values.
        /// </summary>
        public static implicit operator List<ulong>(ListId ids) => ids.Select(id => (ulong)id).ToList();

        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="Id"/> values.
        /// </summary>
        public static implicit operator Collection<Id>(ListId ids) => new(ids.ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of strings.
        /// </summary>
        public static implicit operator Collection<string>(ListId ids) => new(ids.Select(id => id.ToString()).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="Guid"/> values.
        /// </summary>
        public static implicit operator Collection<Guid>(ListId ids) => new(ids.Select(id => id.ToGuid()).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="short"/> values.
        /// </summary>
        public static implicit operator Collection<short>(ListId ids) => new(ids.Select(id => (short)id).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="ushort"/> values.
        /// </summary>
        public static implicit operator Collection<ushort>(ListId ids) => new(ids.Select(id => (ushort)id).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="int"/> values.
        /// </summary>
        public static implicit operator Collection<int>(ListId ids) => new(ids.Select(id => (int)id).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="uint"/> values.
        /// </summary>
        public static implicit operator Collection<uint>(ListId ids) => new(ids.Select(id => (uint)id).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="long"/> values.
        /// </summary>
        public static implicit operator Collection<long>(ListId ids) => new(ids.Select(id => (long)id).ToList());
        /// <summary>
        /// Converts a <see cref="ListId"/> to a collection of <see cref="ulong"/> values.
        /// </summary>
        public static implicit operator Collection<ulong>(ListId ids) => new(ids.Select(id => (ulong)id).ToList());

        #endregion "Operators From"
    }
}
