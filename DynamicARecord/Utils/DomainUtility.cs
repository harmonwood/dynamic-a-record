using Nager.PublicSuffix;
using Microsoft.Extensions.Caching.Memory;

public class DomainUtility
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);
    private const string CacheKey = "PublicSuffixRules";

    public DomainUtility(IMemoryCache cache)
    {
        _cache = cache;
    }

    public (string? Domain, string? Subdomain) SplitDomain(string fqdn)
    {
        var domainParser = new DomainParser(GetTldRules());
        var domainName = domainParser?.Parse(fqdn);

        if (domainName == null) return (null, null);

        return (domainName.RegistrableDomain, domainName.SubDomain);
    }

    private ITldRuleProvider? GetTldRules()
    {
        if (!_cache.TryGetValue<ITldRuleProvider>(CacheKey, out var tldRules))
        {
            tldRules = new WebTldRuleProvider();
            _cache.Set(CacheKey, tldRules, _cacheDuration);
        }

        return tldRules;
    }
}
