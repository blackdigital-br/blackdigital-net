

using System.Collections.ObjectModel;

namespace BlackDigital.Model
{
    public class ListId : EnumerableId, IList<Id>
    {
        #region "Constructors"

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

        #region "Properties"

        protected List<Id> CurrentList => (List<Id>)this.Ids;

        #endregion "Properties"

        #region "IList"

        public Id this[int index] 
        { 
            get => CurrentList[index];
            set => CurrentList[index] = value;
        }

        public int Count => CurrentList.Count;

        public bool IsReadOnly => false;

        public void Add(Id item) => CurrentList.Add(item);

        public void Clear() => CurrentList.Clear();

        public bool Contains(Id item) => CurrentList.Contains(item);

        public void CopyTo(Id[] array, int arrayIndex) => CurrentList.CopyTo(array, arrayIndex);

        public int IndexOf(Id item) => CurrentList.IndexOf(item);

        public void Insert(int index, Id item) => CurrentList.Insert(index, item);

        public bool Remove(Id item) => CurrentList.Remove(item);

        public void RemoveAt(int index) => CurrentList.RemoveAt(index);

        #endregion "IList"

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

        public static implicit operator ListId(List<Id> ids) => new(ids);
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

        public static implicit operator Id[](ListId ids) => ids.Ids.ToArray();
        public static implicit operator string[](ListId ids) => ids.Ids.Select(id => id.ToString()).ToArray();
        public static implicit operator Guid[](ListId ids) => ids.Ids.Select(id => id.ToGuid()).ToArray();
        public static implicit operator short[](ListId ids) => ids.Ids.Select(id => (short)id).ToArray();
        public static implicit operator ushort[](ListId ids) => ids.Ids.Select(id => (ushort)id).ToArray();
        public static implicit operator int[](ListId ids) => ids.Ids.Select(id => (int)id).ToArray();
        public static implicit operator uint[](ListId ids) => ids.Ids.Select(id => (uint)id).ToArray();
        public static implicit operator long[](ListId ids) => ids.Ids.Select(id => (long)id).ToArray();
        public static implicit operator ulong[](ListId ids) => ids.Ids.Select(id => (ulong)id).ToArray();

        public static implicit operator List<Id>(ListId ids) => ids.CurrentList;
        public static implicit operator List<string>(ListId ids) => ids.Ids.Select(id => id.ToString()).ToList();
        public static implicit operator List<Guid>(ListId ids) => ids.Ids.Select(id => id.ToGuid()).ToList();
        public static implicit operator List<short>(ListId ids) => ids.Ids.Select(id => (short)id).ToList();
        public static implicit operator List<ushort>(ListId ids) => ids.Ids.Select(id => (ushort)id).ToList();
        public static implicit operator List<int>(ListId ids) => ids.Ids.Select(id => (int)id).ToList();
        public static implicit operator List<uint>(ListId ids) => ids.Ids.Select(id => (uint)id).ToList();
        public static implicit operator List<long>(ListId ids) => ids.Ids.Select(id => (long)id).ToList();
        public static implicit operator List<ulong>(ListId ids) => ids.Ids.Select(id => (ulong)id).ToList();

        public static implicit operator Collection<Id>(ListId ids) => new(ids.Ids.ToList());
        public static implicit operator Collection<string>(ListId ids) => new(ids.Ids.Select(id => id.ToString()).ToList());
        public static implicit operator Collection<Guid>(ListId ids) => new(ids.Ids.Select(id => id.ToGuid()).ToList());
        public static implicit operator Collection<short>(ListId ids) => new(ids.Ids.Select(id => (short)id).ToList());
        public static implicit operator Collection<ushort>(ListId ids) => new(ids.Ids.Select(id => (ushort)id).ToList());
        public static implicit operator Collection<int>(ListId ids) => new(ids.Ids.Select(id => (int)id).ToList());
        public static implicit operator Collection<uint>(ListId ids) => new(ids.Ids.Select(id => (uint)id).ToList());
        public static implicit operator Collection<long>(ListId ids) => new(ids.Ids.Select(id => (long)id).ToList());
        public static implicit operator Collection<ulong>(ListId ids) => new(ids.Ids.Select(id => (ulong)id).ToList());

        #endregion "Operators From"
    }
}
