using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class AuditModelTest
    {
        [Fact]
        public void CreatedModelShouldImplementICreated()
        {
            var model = new CreatedModel { Created = new DateTime(2020, 1, 1) };

            Assert.IsAssignableFrom<ICreated>(model);
            Assert.Equal(new DateTime(2020, 1, 1), model.Created);
        }

        [Fact]
        public void UpdatedModelShouldImplementIUpdated()
        {
            var model = new UpdatedModel
            {
                Created = new DateTime(2020, 1, 1),
                Updated = new DateTime(2020, 2, 1)
            };

            Assert.IsAssignableFrom<IUpdated>(model);
            Assert.IsAssignableFrom<ICreated>(model);
            Assert.Equal(new DateTime(2020, 1, 1), model.Created);
            Assert.Equal(new DateTime(2020, 2, 1), model.Updated);
        }

        [Fact]
        public void DeletedModelShouldImplementIDeleted()
        {
            var model = new DeletedModel();

            Assert.IsAssignableFrom<IDeleted>(model);
            Assert.IsAssignableFrom<IUpdated>(model);
            Assert.IsAssignableFrom<ICreated>(model);
            Assert.Null(model.Deleted);

            model.Deleted = new DateTime(2020, 3, 1);

            Assert.Equal(new DateTime(2020, 3, 1), model.Deleted);
        }

        [Fact]
        public void AuditDatesShouldBeRequired()
        {
            Assert.NotNull(GetRequiredAttribute<CreatedModel>(nameof(CreatedModel.Created)));
            Assert.NotNull(GetRequiredAttribute<UpdatedModel>(nameof(UpdatedModel.Updated)));
            Assert.Null(GetRequiredAttribute<DeletedModel>(nameof(DeletedModel.Deleted)));
        }

        private static RequiredAttribute? GetRequiredAttribute<TModel>(string propertyName)
            => typeof(TModel).GetProperty(propertyName)?.GetCustomAttribute<RequiredAttribute>();
    }
}
