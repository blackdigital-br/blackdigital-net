

using System.Collections.ObjectModel;

namespace BlackDigital.Model
{
    public class ListId : List<Id>
    {
        #region "Constructors"

        public ListId()
            : base(new List<Id>())
        {
        }

        public ListId(IEnumerable<Id> ids)
            : base(ids.ToList())
        {
        }

        public ListId(IEnumerable<string> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<Guid> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<short> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<ushort> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<int> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<uint> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<long> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        public ListId(IEnumerable<ulong> ids)
            : base(ids.Select(id => new Id(id)).ToList())
        {
        }

        #endregion "Constructors"


        #region "Operators To"

        public static implicit operator ListId(Id[] ids) => new(ids);
        public static implicit operator ListId(string[] ids) => new(ids);
        public static implicit operator ListId(Guid[] ids) => new(ids);
        public static implicit operator ListId(short[] ids) => new(ids);
        public static implicit operator ListId(ushort[] ids) => new(ids);
        public static implicit operator ListId(int[] ids) => new(ids);
        public static implicit operator ListId(uint[] ids) => new(ids);
        public static implicit operator ListId(long[] ids) => new(ids);
        public static implicit operator ListId(ulong[] ids) => new(ids);

        public static implicit operator ListId(List<string> ids) => new(ids);
        public static implicit operator ListId(List<Guid> ids) => new(ids);
        public static implicit operator ListId(List<short> ids) => new(ids);
        public static implicit operator ListId(List<ushort> ids) => new(ids);
        public static implicit operator ListId(List<int> ids) => new(ids);
        public static implicit operator ListId(List<uint> ids) => new(ids);
        public static implicit operator ListId(List<long> ids) => new(ids);
        public static implicit operator ListId(List<ulong> ids) => new(ids);

        public static implicit operator ListId(Collection<Id> ids) => new(ids);
        public static implicit operator ListId(Collection<string> ids) => new(ids);
        public static implicit operator ListId(Collection<Guid> ids) => new(ids);
        public static implicit operator ListId(Collection<short> ids) => new(ids);
        public static implicit operator ListId(Collection<ushort> ids) => new(ids);
        public static implicit operator ListId(Collection<int> ids) => new(ids);
        public static implicit operator ListId(Collection<uint> ids) => new(ids);
        public static implicit operator ListId(Collection<long> ids) => new(ids);
        public static implicit operator ListId(Collection<ulong> ids) => new(ids);

        #endregion "Operators To"

        #region "Operators From"

        public static implicit operator Id[](ListId ids) => ids.ToArray();
        public static implicit operator string[](ListId ids) => ids.Select(id => id.ToString()).ToArray();
        public static implicit operator Guid[](ListId ids) => ids.Select(id => id.ToGuid()).ToArray();
        public static implicit operator short[](ListId ids) => ids.Select(id => (short)id).ToArray();
        public static implicit operator ushort[](ListId ids) => ids.Select(id => (ushort)id).ToArray();
        public static implicit operator int[](ListId ids) => ids.Select(id => (int)id).ToArray();
        public static implicit operator uint[](ListId ids) => ids.Select(id => (uint)id).ToArray();
        public static implicit operator long[](ListId ids) => ids.Select(id => (long)id).ToArray();
        public static implicit operator ulong[](ListId ids) => ids.Select(id => (ulong)id).ToArray();

        public static implicit operator List<string>(ListId ids) => ids.Select(id => id.ToString()).ToList();
        public static implicit operator List<Guid>(ListId ids) => ids.Select(id => id.ToGuid()).ToList();
        public static implicit operator List<short>(ListId ids) => ids.Select(id => (short)id).ToList();
        public static implicit operator List<ushort>(ListId ids) => ids.Select(id => (ushort)id).ToList();
        public static implicit operator List<int>(ListId ids) => ids.Select(id => (int)id).ToList();
        public static implicit operator List<uint>(ListId ids) => ids.Select(id => (uint)id).ToList();
        public static implicit operator List<long>(ListId ids) => ids.Select(id => (long)id).ToList();
        public static implicit operator List<ulong>(ListId ids) => ids.Select(id => (ulong)id).ToList();
            
        public static implicit operator Collection<Id>(ListId ids) => new(ids.ToList());
        public static implicit operator Collection<string>(ListId ids) => new(ids.Select(id => id.ToString()).ToList());
        public static implicit operator Collection<Guid>(ListId ids) => new(ids.Select(id => id.ToGuid()).ToList());
        public static implicit operator Collection<short>(ListId ids) => new(ids.Select(id => (short)id).ToList());
        public static implicit operator Collection<ushort>(ListId ids) => new(ids.Select(id => (ushort)id).ToList());
        public static implicit operator Collection<int>(ListId ids) => new(ids.Select(id => (int)id).ToList());
        public static implicit operator Collection<uint>(ListId ids) => new(ids.Select(id => (uint)id).ToList());
        public static implicit operator Collection<long>(ListId ids) => new(ids.Select(id => (long)id).ToList());
        public static implicit operator Collection<ulong>(ListId ids) => new(ids.Select(id => (ulong)id).ToList());

        #endregion "Operators From"
    }
}
