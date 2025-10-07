
namespace BlackDigital.Rest.Transforms
{
    public class TransformManager
    {
        private readonly Dictionary<TransformKey, ITransformRule> _rules = new();

        public TransformManager AddRule(TransformKey key, ITransformRule rule)
        {
            if (_rules.ContainsKey(key))
                throw new ArgumentException($"Rule with key '{key}' already exists.");

            _rules.Add(key, rule);

            return this;
        }

        public TransformManager AddRule(string key, string version, ITransformRule rule, TransformDirection direction = TransformDirection.Input)
            => AddRule(new TransformKey(key, version, direction), rule);

        public TransformManager AddRule(ITransformRule rule, params TransformKey[] keys)
        {
            if (keys.Length == 0)
                throw new ArgumentException("At least one key must be provided.");

            foreach (var key in keys)
                AddRule(key, rule);

            return this;
        }


        public bool HasRule(TransformKey key)
            => _rules.ContainsKey(key);

        public bool HasRule(string key, string version, TransformDirection direction = TransformDirection.Input)
            => HasRule(new TransformKey(key, version, direction));

        public ITransformRule? GetRule(TransformKey key)
        {
            _rules.TryGetValue(key, out ITransformRule? rule);
            return rule;
        }

        public ITransformRule? GetRule(string key, string version, TransformDirection direction = TransformDirection.Input)
            => GetRule(new TransformKey(key, version, direction));

        public bool TryAddRule(TransformKey key, ITransformRule rule)
        {
            if (_rules.ContainsKey(key))
                return false;

            _rules.Add(key, rule);
            return true;
        }

        public bool TryAddRule(string key, string version, ITransformRule rule, TransformDirection direction = TransformDirection.Input)
            => TryAddRule(new TransformKey(key, version, direction), rule);

        public TransformManager ReplaceRule(TransformKey key, ITransformRule rule)
        {
            _rules[key] = rule;
            return this;
        }

        public TransformManager ReplaceRule(string key, string version, ITransformRule rule, TransformDirection direction = TransformDirection.Input)
            => ReplaceRule(new TransformKey(key, version, direction), rule);

        public bool RemoveRule(TransformKey key)
            => _rules.Remove(key);

        public bool RemoveRule(string key, string version, TransformDirection direction = TransformDirection.Input)
            => RemoveRule(new TransformKey(key, version, direction));

        public bool TryGetRule(TransformKey key, out ITransformRule? rule)
            => _rules.TryGetValue(key, out rule);

        public bool TryGetRule(string key, string version, out ITransformRule? rule, TransformDirection direction = TransformDirection.Input)
            => TryGetRule(new TransformKey(key, version, direction), out rule);

        public ITransformRule GetRequiredRule(TransformKey key)
        {
            if (!_rules.TryGetValue(key, out ITransformRule? rule))
                throw new KeyNotFoundException($"Rule with key '{key}' not found.");

            return rule;
        }

        public ITransformRule GetRequiredRule(string key, string version, TransformDirection direction = TransformDirection.Input)
            => GetRequiredRule(new TransformKey(key, version, direction));

        public IList<ITransformRule> GetRequiredRules(TransformKey key)
        {
            var matchingRules = _rules
                .Where(kvp => kvp.Key.Key == key.Key && 
                             kvp.Key.Direction == key.Direction && 
                             string.Compare(kvp.Key.Version, key.Version, StringComparison.Ordinal) > 0)
                .OrderBy(kvp => kvp.Key.Version)
                .Select(kvp => kvp.Value)
                .ToList();

            if (matchingRules.Count == 0)
                throw new KeyNotFoundException($"No rules found with key '{key.Key}', direction '{key.Direction}' and version greater than '{key.Version}'.");

            return matchingRules;
        }

        public IList<ITransformRule> GetRequiredRules(string key, string version, TransformDirection direction = TransformDirection.Input)
            => GetRequiredRules(new TransformKey(key, version, direction));

        public object? Transform(TransformKey key, object? value)
        {
            var rules = GetRequiredRules(key);
            var result = value;

            foreach (var rule in rules)
            {
                result = rule.Transform(result);
            }

            return result;
        }

        public object? Transform(string key, string version, object? value, TransformDirection direction = TransformDirection.Input)
            => Transform(new TransformKey(key, version, direction), value);

        public async Task<object?> TransformAsync(TransformKey key, object? value)
        {
            var rules = GetRequiredRules(key);
            var result = value;

            foreach (var rule in rules)
            {
                result = await rule.TransformAsync(result);
            }

            return result;
        }

        public async Task<object?> TransformAsync(string key, string version, object? value, TransformDirection direction = TransformDirection.Input)
            => await TransformAsync(new TransformKey(key, version, direction), value);
    }
}
