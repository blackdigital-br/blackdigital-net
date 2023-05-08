
using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class IdFilterTest
    {
        private readonly IdFilterTestModel[] _models = new IdFilterTestModel[]
        {
            new IdFilterTestModel(new Id(1)),
            new IdFilterTestModel(new Id(2)),
            new IdFilterTestModel(new Id(3)),
            new IdFilterTestModel(new Id(4)),
            new IdFilterTestModel(new Id(5)),
        };

        private readonly IdFilterTestModel<int>[] _modelsInt = new IdFilterTestModel<int>[]
        {
            new IdFilterTestModel<int>(1),
            new IdFilterTestModel<int>(2),
            new IdFilterTestModel<int>(3),
            new IdFilterTestModel<int>(4),
            new IdFilterTestModel<int>(5),
        };

        [Fact]
        public void FilterIdModel()
        {
            var query = _models.AsQueryable().FilterId(1);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FilterId(2);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FilterId(3);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FilterId(4);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FilterId(5);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FilterId(6);
            Assert.Equal(0, query.Count());
            query = _models.AsQueryable().FilterId(null);
            Assert.Equal(5, query.Count());
        }

        [Fact]
        public void FindIdModel()
        {
            var query = _models.AsQueryable().FindId(1);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FindId(2);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FindId(3);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FindId(4);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FindId(5);
            Assert.Equal(1, query.Count());
            query = _models.AsQueryable().FindId(6);
            Assert.Equal(0, query.Count());
        }

        [Fact]
        public void FilterIdKeyModel()
        {
            var query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(1);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(2);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(3);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(4);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(5);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(6);
            Assert.Equal(0, query.Count());
            query = _modelsInt.AsQueryable().FilterId<IdFilterTestModel<int>, int>(null);
            Assert.Equal(5, query.Count());
        }

        [Fact]
        public void FindIdKeyModel()
        {
            var query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(1);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(2);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(3);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(4);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(5);
            Assert.Equal(1, query.Count());
            query = _modelsInt.AsQueryable().FindId<IdFilterTestModel<int>, int>(6);
            Assert.Equal(0, query.Count());
        }


        public class IdFilterTestModel : IId
        {
            public IdFilterTestModel() { }

            public IdFilterTestModel(Id id)
            {
                Id = id;
            }

            public Id Id { get; set; }
        }

        public class IdFilterTestModel<TKey> : IId<TKey>
            where TKey : struct
        {
            public IdFilterTestModel() { }
            public IdFilterTestModel(TKey id)
            {
                Id = id;
            }
            public TKey Id { get; set; }
        }
    }
}
