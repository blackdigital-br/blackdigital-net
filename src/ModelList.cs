
namespace BlackDigital
{
    public class ModelList<T>
        where T : class
    {
        public IEnumerable<T> Itens { get; init; } = Array.Empty<T>();

        public int TotalItens { get; init; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalItens / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
