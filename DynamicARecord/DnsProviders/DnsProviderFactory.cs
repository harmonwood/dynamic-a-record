namespace DynamicARecord.DnsProviders
{
    public class DnsProviderFactory
    {
        // Delegate type for factory methods
        private delegate DnsProviderBase ProviderFactory(params string[] args);

        // Dictionary to map provider keys to their factory methods
        private static readonly Dictionary<string, ProviderFactory> _providerFactories = new Dictionary<string, ProviderFactory>(StringComparer.OrdinalIgnoreCase)
        {
            { NameCheapDnsProvider.Name(), args => new NameCheapDnsProvider(args[0], args[1], args[2]) }
        };

        public static DnsProviderBase CreateProvider(string providerKey, params string[] args)
        {
            if (_providerFactories.TryGetValue(providerKey, out var factory))
            {
                return factory(args);
            }
            throw new ArgumentException($"Unsupported provider: {providerKey}");
        }
    }
}
