
using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class ActiveFilterTest
    {
        private readonly ActiveModelTest[] _models = new ActiveModelTest[]
        {
            new ActiveModelTest(true),
            new ActiveModelTest(false),
            new ActiveModelTest(true),
            new ActiveModelTest(false),
            new ActiveModelTest(true),
        };

        [Fact]
        public void FilterIsActive()
        {
            var query = _models.AsQueryable().IsActive();
            Assert.Equal(3, query.Count());
        }

        [Fact]
        public void FilterNotIsActive()
        {
            var query = _models.AsQueryable().NotIsActive();
            Assert.Equal(2, query.Count());
        }

        [Fact]
        public void FilterActive()
        {
            var query = _models.AsQueryable().FilterActive(true);
            Assert.Equal(3, query.Count());

            query = _models.AsQueryable().FilterActive(false);
            Assert.Equal(2, query.Count());

            query = _models.AsQueryable().FilterActive(null);
            Assert.Equal(5, query.Count());
        }
    }

    public class ActiveModelTest : IActive
    {
        public ActiveModelTest() { }

        public ActiveModelTest(bool active)
        {
            Active = active;
        }

        public bool Active { get; set; }
    }
}
