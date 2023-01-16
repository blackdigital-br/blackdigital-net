
namespace BlackDigital.Test
{
    public class DateTimeHelperTest
    {
        [Fact]
        public void MonthCast()
        {
            var datetime = new DateTime(2023, 1, 5, 14, 26, 44);
            var dateMonth = datetime.ToMonthDate();
            
            Assert.Equal(2023, dateMonth.Year);
            Assert.Equal(1, dateMonth.Month);
            Assert.Equal(1, dateMonth.Day);
            Assert.Equal(0, dateMonth.Hour);
            Assert.Equal(0, dateMonth.Minute);
            Assert.Equal(0, dateMonth.Second);
        }

        [Fact]
        public void ParseDate()
        {
            var onlyDate = "2023-01-20";
            var datetime = onlyDate.ToDateTime();

            Assert.Equal(2023, datetime.Year);
            Assert.Equal(1, datetime.Month);
            Assert.Equal(20, datetime.Day);
            Assert.Equal(0, datetime.Hour);
            Assert.Equal(0, datetime.Minute);
            Assert.Equal(0, datetime.Second);
        }
    }
}
