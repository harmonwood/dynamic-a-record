using Flurl.Http;

namespace DynamicARecord.IpAddressProviders
{
    public class IpInfoAddressProvider : IIpAddressProvider
    {
        public string Name() => "IpInfo";

        public async Task<string> GetExternalIpAddressAsync()
        {
            dynamic response = await "https://ipinfo.io/".GetJsonAsync();
            return response.ip;
        }
    }
}
