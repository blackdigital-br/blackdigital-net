namespace BlackDigital.Test
{
    public class ModelListTest
    {
        [Fact]
        public void PaginationProperties_ReturnExpectedValues()
        {
            var result = new ModelList<string>
            {
                Itens = new[] { "A", "B" },
                TotalItens = 25,
                PageNumber = 2,
                PageSize = 10,
            };

            Assert.Equal(3, result.TotalPages);
            Assert.True(result.HasPreviousPage);
            Assert.True(result.HasNextPage);
        }

        [Fact]
        public void PaginationProperties_WithInvalidPageSize_ReturnNoPages()
        {
            var result = new ModelList<string>
            {
                TotalItens = 25,
                PageNumber = 1,
                PageSize = 0,
            };

            Assert.Equal(0, result.TotalPages);
            Assert.False(result.HasPreviousPage);
            Assert.False(result.HasNextPage);
        }
    }
}
