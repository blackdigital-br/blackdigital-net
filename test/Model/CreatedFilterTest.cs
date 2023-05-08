
using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class CreatedFilterTest
    {
        private readonly CreatedModelTest[] _models = new CreatedModelTest[]
        {
            new CreatedModelTest(new DateTime(2020, 1, 1)),
            new CreatedModelTest(new DateTime(2023, 11, 5)),
            new CreatedModelTest(new DateTime(2020, 1, 11)),
            new CreatedModelTest(new DateTime(2020, 2, 1)),
            new CreatedModelTest(new DateTime(2020, 1, 2))
        };

        [Fact]
        public void FindCreated()
        {
            IQueryable<CreatedModelTest> query = _models.AsQueryable();
            DateTime date = new(2020, 1, 1);
            query = query.Where(model => model.Created == date);
            //Assert.Single(query);
        }

        [Fact]
        public void FilterCreated()
        {
            var query = _models.AsQueryable().FilterCreated(new DateTime(2020, 1, 1));
            //Assert.Single(query);
            query = _models.AsQueryable().FilterCreated(new DateTime(2020, 1, 1));
            //Assert.Single(query);
            query = _models.AsQueryable().FilterCreated(null);
            //Assert.Equal(5, query.Count());
        }

        [Fact]
        public void FilterMaxCreated()
        {
            var query = _models.AsQueryable().FilterMaxCreated(new DateTime(2020, 1, 1));
            //Assert.Equal(3, query.Count());
            query = _models.AsQueryable().FilterMaxCreated(new DateTime(2020, 1, 2));
            //Assert.Equal(4, query.Count());
            query = _models.AsQueryable().FilterMaxCreated(null);
            //Assert.Equal(5, query.Count());
        }

        [Fact]
        public void FilterMinCreated()
        {
            var query = _models.AsQueryable().FilterMinCreated(new DateTime(2020, 1, 1));
            //Assert.Equal(5, query.Count());
            query = _models.AsQueryable().FilterMinCreated(new DateTime(2020, 1, 2));
            //Assert.Equal(4, query.Count());
            query = _models.AsQueryable().FilterMinCreated(null);
            //Assert.Equal(5, query.Count());
        }

        [Fact]
        public void FilterCreatedRange()
        {
            var query = _models.AsQueryable().FilterCreatedRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 2));
            //Assert.Equal(3, query.Count());
            query = _models.AsQueryable().FilterCreatedRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1));
            //Assert.Single(query);
            query = _models.AsQueryable().FilterCreatedRange(new DateTime(2020, 1, 2), new DateTime(2020, 1, 1));
            //Assert.Empty(query);
            query = _models.AsQueryable().FilterCreatedRange(null, null);
            //Assert.Equal(5, query.Count());
        }

        public class CreatedModelTest : ICreated
        {
            public CreatedModelTest() { }

            public CreatedModelTest(DateTime created) 
            { 
                Created = created;
            }

            public DateTime Created { get; set; }
        }
    }
}
