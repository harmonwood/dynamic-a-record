using DynamicARecord.DnsProviders;
using DynamicARecord.IpAddressProviders;
using Microsoft.Extensions.Caching.Memory;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: DynamicARecord <provider> <provider_args...> <subdomain> [ip_address]");
            return;
        }

        var providerKey = args[0];
        var fqdn = args[args.Length - 2];
        var ipAddress = args.Length > 4 ? args[args.Length - 1] : null;

        IIpAddressProvider ipAddressProvider = new IpInfoAddressProvider();
        DnsProviderBase dnsProvider = DnsProviderFactory.CreateProvider(providerKey, args.Skip(1).Take(args.Length - 3).ToArray());
        IMemoryCache memcache = new MemoryCache(new MemoryCacheOptions());
        DomainUtility domainUtility = new DomainUtility(memcache);

        // parse domain and subdomain from FQDN
        var (domain, subdomain) = domainUtility.SplitDomain(fqdn);
        if (domain == null || subdomain == null)
        {
            Console.WriteLine($"Failed to parse domain from {fqdn}");
            return;
        }

        // check if IP address is provided, if not, get it from IP address provider
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = await ipAddressProvider.GetExternalIpAddressAsync();
        }

        // bail if new reason to update.
        if (DnsResolver.validIpAddress(fqdn, ipAddress))
        {
            Console.WriteLine($"DNS record for {fqdn} is already up to date.");
            return;
        }

        var success = await dnsProvider.UpdateDnsRecordAsync(domain, subdomain, ipAddress);
        if (success)
        {
            Console.WriteLine($"DNS record for {subdomain} updated to IP: {ipAddress}");
        }
        else
        {
            Console.WriteLine($"Failed to update DNS record for {subdomain}.");
        }
    }
}
