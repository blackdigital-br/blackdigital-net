
namespace BlackDigital
{
    public record ModelList<T>
        where T : class
    {

        public IEnumerable<T> Itens { get; init; }

        public int TotalItens { get; init; }
    }
}
