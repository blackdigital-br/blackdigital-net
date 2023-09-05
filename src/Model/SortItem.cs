
namespace BlackDigital.Model
{
    public class SortItem
    {
        public SortItem() { }

        public SortItem(string propertyName, bool isAscending = true)
        {
            Name = propertyName;
            Asc = isAscending;
        }

        public string Name { get; set; }

        public bool Asc { get; set; }
    }
}
