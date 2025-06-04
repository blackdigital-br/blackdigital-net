using BlackDigital.Test.Mock;


namespace BlackDigital.Test
{
    public class ExpressionHelperTest
    {
        [Fact(DisplayName = "Get Multiple Properties From Expression")]
        public void GetMonthFromMidDate()
        {
            var properties = ExpressionHelper.GetPropertiesInfoFromExpression<ComplexModel>(x => new { x.Id, x.Name });

            Assert.NotNull(properties);
            Assert.Equal(2, properties.Count());
            Assert.Contains(properties, p => p.Name == "Id");
            Assert.Contains(properties, p => p.Name == "Name");
        }

        [Fact(DisplayName = "Get Single Property From Expression")]
        public void GetSinglePropertyFromExpression()
        {
            var properties = ExpressionHelper.GetPropertiesInfoFromExpression<ComplexModel>(x => x.Id);
            Assert.NotNull(properties);
            Assert.Single(properties);
            Assert.Contains(properties, p => p.Name == "Id");
        }

    }
}
