
using System.Collections;

namespace BlackDigital.Model
{
    public class Options : IEnumerable<OptionItem>
    {
        public Options()
        {
            Items = new OptionItem[0];
        }

        public Options(IEnumerable<OptionItem> items)
        {
            Items = items.ToArray();
        }

        private OptionItem[] Items { get; set; }

        public OptionItem? this[int key]
        {
            get
            {
                if (Items.Any(option => option.Id == key))
                    return this.FirstOrDefault(o => o.Id == key);

                return null;
            }
        }

        public bool ContainsKey(int key)
        {
            return Items.Any(option => option.Id == key);
        }

        public Options FilterParents(string name, Id id)
        {
            return new Options(Items.Where(option => option.Parents.ContainsKey(name)
                                && option.Parents[name] == id));
        }

        public Options FilterLabel(string label)
        {
            return new Options(Items.Where(option => option.Label.Contains(label, StringComparison.InvariantCultureIgnoreCase)));
        }

        public Options FindText(string text)
        {
            return new Options(Items.Where(option =>
                            option.Label.Contains(text, StringComparison.InvariantCultureIgnoreCase)
                            || (option.Code?.Contains(text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                            || (option.Description?.Contains(text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                        ));
        }

        public Options FilterParentsId(Id id)
        {
            return new Options(
                Items.Where(option => option.Parents.Any(parent => parent.Value == id))
            );
        }

        public IEnumerator<OptionItem> GetEnumerator() => ((IEnumerable<OptionItem>)Items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();
    }
}
