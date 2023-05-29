using System.Collections;
using System.Collections.ObjectModel;

namespace BlackDigital.Model
{
    public class EnumerableId : IEnumerable<Id>
    {
        #region "Constructors"

        public EnumerableId(IEnumerable<Id> ids)
        {
            Ids = ids;
        }

        public EnumerableId(IEnumerable<string> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<Guid> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<short> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<ushort> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<int> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<uint> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<long> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        public EnumerableId(IEnumerable<ulong> ids)
        {
            Ids = ids.Select(id => new Id(id));
        }

        #endregion "Constructors"

        #region "Properties"

        protected readonly IEnumerable<Id> Ids;

        #endregion "Properties"

        #region "IEnumerable"

        public IEnumerator<Id> GetEnumerator() => Ids.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion "IEnumerable"

        #region "Overriden Methods"

        override public string ToString() => string.Join(", ", Ids.Select(id => id.ToString()));
        override public int GetHashCode() => Ids.GetHashCode();
        override public bool Equals(object? obj) => obj is EnumerableId enumerableId && Ids.Equals(enumerableId.Ids);

        #endregion "Overriden Methods"

        #region "Operators To"

        public static implicit operator EnumerableId(Id[] ids) => new(ids);
        public static implicit operator EnumerableId(string[] ids) => new(ids);
        public static implicit operator EnumerableId(Guid[] ids) => new(ids);
        public static implicit operator EnumerableId(short[] ids) => new(ids);
        public static implicit operator EnumerableId(ushort[] ids) => new(ids);
        public static implicit operator EnumerableId(int[] ids) => new(ids);
        public static implicit operator EnumerableId(uint[] ids) => new(ids);
        public static implicit operator EnumerableId(long[] ids) => new(ids);
        public static implicit operator EnumerableId(ulong[] ids) => new(ids);

        public static implicit operator EnumerableId(List<Id> ids) => new(ids);
        public static implicit operator EnumerableId(List<string> ids) => new(ids);
        public static implicit operator EnumerableId(List<Guid> ids) => new(ids);
        public static implicit operator EnumerableId(List<short> ids) => new(ids);
        public static implicit operator EnumerableId(List<ushort> ids) => new(ids);
        public static implicit operator EnumerableId(List<int> ids) => new(ids);
        public static implicit operator EnumerableId(List<uint> ids) => new(ids);
        public static implicit operator EnumerableId(List<long> ids) => new(ids);
        public static implicit operator EnumerableId(List<ulong> ids) => new(ids);

        public static implicit operator EnumerableId(Collection<Id> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<string> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<Guid> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<short> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<ushort> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<int> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<uint> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<long> ids) => new(ids);
        public static implicit operator EnumerableId(Collection<ulong> ids) => new(ids);

        #endregion "Operators To"

        #region "Operators From"

        public static implicit operator Id[](EnumerableId ids) => ids.Ids.ToArray();
        public static implicit operator string[](EnumerableId ids) => ids.Ids.Select(id => id.ToString()).ToArray();
        public static implicit operator Guid[](EnumerableId ids) => ids.Ids.Select(id => id.ToGuid()).ToArray();
        public static implicit operator short[](EnumerableId ids) => ids.Ids.Select(id => (short)id).ToArray();
        public static implicit operator ushort[](EnumerableId ids) => ids.Ids.Select(id => (ushort)id).ToArray();
        public static implicit operator int[](EnumerableId ids) => ids.Ids.Select(id => (int)id).ToArray();
        public static implicit operator uint[](EnumerableId ids) => ids.Ids.Select(id => (uint)id).ToArray();
        public static implicit operator long[](EnumerableId ids) => ids.Ids.Select(id => (long)id).ToArray();
        public static implicit operator ulong[](EnumerableId ids) => ids.Ids.Select(id => (ulong)id).ToArray();

        public static implicit operator List<Id>(EnumerableId ids) => ids.Ids.ToList();
        public static implicit operator List<string>(EnumerableId ids) => ids.Ids.Select(id => id.ToString()).ToList();
        public static implicit operator List<Guid>(EnumerableId ids) => ids.Ids.Select(id => id.ToGuid()).ToList();
        public static implicit operator List<short>(EnumerableId ids) => ids.Ids.Select(id => (short)id).ToList();
        public static implicit operator List<ushort>(EnumerableId ids) => ids.Ids.Select(id => (ushort)id).ToList();
        public static implicit operator List<int>(EnumerableId ids) => ids.Ids.Select(id => (int)id).ToList();
        public static implicit operator List<uint>(EnumerableId ids) => ids.Ids.Select(id => (uint)id).ToList();
        public static implicit operator List<long>(EnumerableId ids) => ids.Ids.Select(id => (long)id).ToList();
        public static implicit operator List<ulong>(EnumerableId ids) => ids.Ids.Select(id => (ulong)id).ToList();

        public static implicit operator Collection<Id>(EnumerableId ids) => new(ids.Ids.ToList());
        public static implicit operator Collection<string>(EnumerableId ids) => new(ids.Ids.Select(id => id.ToString()).ToList());
        public static implicit operator Collection<Guid>(EnumerableId ids) => new(ids.Ids.Select(id => id.ToGuid()).ToList());
        public static implicit operator Collection<short>(EnumerableId ids) => new(ids.Ids.Select(id => (short)id).ToList());
        public static implicit operator Collection<ushort>(EnumerableId ids) => new(ids.Ids.Select(id => (ushort)id).ToList());
        public static implicit operator Collection<int>(EnumerableId ids) => new(ids.Ids.Select(id => (int)id).ToList());
        public static implicit operator Collection<uint>(EnumerableId ids) => new(ids.Ids.Select(id => (uint)id).ToList());
        public static implicit operator Collection<long>(EnumerableId ids) => new(ids.Ids.Select(id => (long)id).ToList());
        public static implicit operator Collection<ulong>(EnumerableId ids) => new(ids.Ids.Select(id => (ulong)id).ToList());

        #endregion "Operators From"
    }
}
