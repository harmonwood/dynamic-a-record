namespace DynamicARecord.DnsProviders
{
    public abstract class DnsProviderBase
    {
       public abstract Task<bool> UpdateDnsRecordAsync(string zoneId, string subdomain, string ipAddress);
       public static string Name => "default";
    }
}