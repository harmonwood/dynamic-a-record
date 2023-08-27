using Flurl;
using Flurl.Http;

namespace DynamicARecord.DnsProviders
{
    public class NameCheapDnsProvider : DnsProviderBase
    {
        private const string BaseUrl = "https://api.namecheap.com/xml.response";
        private readonly string _apiUser;
        private readonly string _apiKey;
        private readonly string _clientIp;

        public static new string Name() => "NameCheap";

        public NameCheapDnsProvider(string apiUser, string apiKey, string clientIp)
        {
            _apiUser = apiUser;
            _apiKey = apiKey;
            _clientIp = clientIp;
        }

        public override async Task<bool> UpdateDnsRecordAsync(string domain, string subdomain, string ipAddress)
        {
            await BaseUrl
                .SetQueryParam("ApiUser", _apiUser)
                .SetQueryParam("ApiKey", _apiKey)
                .SetQueryParam("UserName", _apiUser)
                .SetQueryParam("ClientIp", _clientIp)
                .SetQueryParam("Command", "namecheap.domains.dns.setHosts")
                .SetQueryParam("SLD", domain.Split('.')[0]) // Assuming domain is something like "example.com"
                .SetQueryParam("TLD", domain.Split('.')[1])
                .SetQueryParam("HostName1", subdomain)
                .SetQueryParam("RecordType1", "A")
                .SetQueryParam("Address1", ipAddress)
                .PostAsync(null);

            return true;
        }
    }
}
