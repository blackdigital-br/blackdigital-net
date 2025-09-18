
namespace BlackDigital.Model
{
    public class SortItem
    {
        public SortItem() { }

        public SortItem(string propertyName, bool? sortAscending = null)
        {
            Name = propertyName;
            SortAscending = sortAscending;
        }

        public string Name { get; set; }

        public bool? SortAscending { get; set; }
    }
}
