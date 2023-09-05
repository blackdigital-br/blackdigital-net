
using BlackDigital.Model;

namespace BlackDigital.Test
{
    public class IdTest
    {
        [Fact]
        public void CastWithString()
        {
            string text = "ABC1234";
            Id id = text;

            Assert.Equal(text, id);
            Assert.True(id.Equals(text));

            string value = id;

            Assert.Equal(text, value);
        }

        [Fact]
        public void CastWithGuid()
        {
            Guid guid = Guid.NewGuid();
            Id id = guid;

            Assert.True(id.Equals((object)guid));
            Assert.True(id.Equals(guid));

            Guid value = id;

            Assert.Equal(guid, value);
        }

        [Fact]
        public void CastWithShort()
        {
            short number = short.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            short value = id;

            Assert.Equal(number, value);
        }

        [Fact]
        public void CastWithUShort()
        {
            ushort number = ushort.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            ushort value = id;

            Assert.Equal(number, value);
        }

        [Fact]
        public void CastWithInt()
        {
            int number = int.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            int value = id;

            Assert.Equal(number, value);
        }

        [Fact]
        public void CastWithUInt()
        {
            uint number = uint.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            uint value = id;

            Assert.Equal(number, value);
        }

        [Fact]
        public void CastWithLong()
        {
            long number = long.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            long value = id;

            Assert.Equal(number, value);
        }

        [Fact]
        public void CastWithULong()
        {
            ulong number = ulong.MaxValue;
            Id id = number;

            Assert.True(id.Equals((object)number));
            Assert.True(id.Equals(number));

            ulong value = id;

            Assert.Equal(number, value);
        }
    }
}
