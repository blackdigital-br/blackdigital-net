namespace BlackDigital.Test
{
    public class EnumHelperTest
    {
        private enum SampleStatus
        {
            Pending = 1,
            Approved = 2,
        }

        [Fact]
        public void IsAndIsNot_WithEnumValue_WorkAsExpected()
        {
            var value = SampleStatus.Approved;

            Assert.True(value.Is(SampleStatus.Approved));
            Assert.False(value.Is(SampleStatus.Pending));
            Assert.True(value.IsNot(SampleStatus.Pending));
            Assert.False(value.IsNot(SampleStatus.Approved));
        }

        [Fact]
        public void IsAndIsNot_WithIntegerValue_WorkAsExpected()
        {
            var value = SampleStatus.Approved;

            Assert.True(value.Is(2));
            Assert.False(value.Is(1));
            Assert.True(value.IsNot(1));
            Assert.False(value.IsNot(2));
        }

        [Fact]
        public void IsAndIsNot_WithStringValue_RespectIgnoreCase()
        {
            var value = SampleStatus.Approved;

            Assert.True(value.Is("Approved"));
            Assert.False(value.Is("approved"));
            Assert.True(value.Is("approved", ignoreCase: true));
            Assert.True(value.IsNot("Pending"));
            Assert.True(value.IsNot("invalid"));
        }
    }
}
