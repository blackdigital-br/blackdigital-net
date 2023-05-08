
using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class BaseFilterFilterTest
    {
        private readonly string[] _datas = new string[]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", 
            "j", "k", "l", "m", "n", "o", "p", "q", "r",
            "s", "t", "u", "v", "w", "x", "y", "z"
        };

        [Fact]
        public void ApplyFilter()
        {
            IQueryable<string> query = _datas.AsQueryable();
            BaseFilter filter = new()
            {
                Skip = 1,
                Take = 2,
                Sort = new List<SortItem>
                {
                    new SortItem { Name = "Length", Asc = true }
                }
            };


            query = query.ApplyFilter(filter);
            Assert.Equal(2, query.Count());
        }
    }
}
