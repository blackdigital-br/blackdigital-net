
namespace BlackDigital.Model
{
    public class BaseFilter
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public List<SortItem> Sort { get; set; } = new();
    }
}
