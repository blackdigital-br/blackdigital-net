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
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem
                {
                    Id = 2,
                    Label = "B",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
                new OptionItem
                {
                    Id = 3,
                    Label = "C",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem
                {
                    Id = 4,
                    Label = "D",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
                new OptionItem
                {
                    Id = 5,
                    Label = "E",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem
                {
                    Id = 6,
                    Label = "F",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
                new OptionItem
                {
                    Id = 7,
                    Label = "G",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem
                {
                    Id = 8,
                    Label = "H",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
                new OptionItem
                {
                    Id = 9,
                    Label = "I",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem
                {
                    Id = 10,
                    Label = "J",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
            });
            var filtered = options.FilterParents("Parent", 1);
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
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
                new OptionItem()
                {
                    Id = 2,
                    Label = "B",
                    Description = "C",
                    Code = "D",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 2 }
                    }
                },
                new OptionItem()
                {
                    Id = 3,
                    Label = "C",
                    Description = "D",
                    Code = "E",
                    Parents = new Dictionary<string, Id>
                    {
                        { "Parent", 1 }
                    }
                },
            });

            var filtered = options.FindText("B");
            Assert.Equal(2, filtered.Count());
        }
    }
}
