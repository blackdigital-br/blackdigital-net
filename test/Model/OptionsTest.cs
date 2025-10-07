using BlackDigital.Model;

namespace BlackDigital.Test.Model
{
    public class OptionsTest
    {
        [Fact]
        public void FilterParents()
        {
            var options = new Options(new OptionItem[]
            {
                new OptionItem
                {
                    Id = 1,
                    Label = "A",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 1 ] }
                    }
                },
                new OptionItem
                {
                    Id = 2,
                    Label = "B",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 2 ] }
                    }
                },
                new OptionItem
                {
                    Id = 3,
                    Label = "C",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 1 ] }
                    }
                },
                new OptionItem
                {
                    Id = 4,
                    Label = "D",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 2 ] }
                    }
                },
                new OptionItem
                {
                    Id = 5,
                    Label = "E",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 1 ] }
                    }
                },
                new OptionItem
                {
                    Id = 6,
                    Label = "F",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 2 ] }
                    }
                },
                new OptionItem
                {
                    Id = 7,
                    Label = "G",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 1 ] }
                    }
                },
                new OptionItem
                {
                    Id = 8,
                    Label = "H",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 2 ] }
                    }
                },
                new OptionItem
                {
                    Id = 9,
                    Label = "I",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent",  [ 1 ] }
                    }
                },
                new OptionItem
                {
                    Id = 10,
                    Label = "J",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent",  [ 2 ] }
                    }
                },
            });
            var filtered = options.FilterConnections("Parent", 1);
            Assert.Equal(5, filtered.Count());
        }

        [Fact]
        public void FindText()
        {
            var options = new Options(new List<OptionItem>
            {
                new OptionItem()
                {
                    Id = 1,
                    Label = "A",
                    Description = "B",
                    Code = "C",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent",  [ 1 ] }
                    }
                },
                new OptionItem()
                {
                    Id = 2,
                    Label = "B",
                    Description = "C",
                    Code = "D",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent",  [ 2 ] }
                    }
                },
                new OptionItem()
                {
                    Id = 3,
                    Label = "C",
                    Description = "D",
                    Code = "E",
                    Connections = new Dictionary<string, ListId>
                    {
                        { "Parent", [ 1 ] }
                    }
                },
            });

            var filtered = options.FindText("B");
            Assert.Equal(2, filtered.Count());
        }
    }
}
