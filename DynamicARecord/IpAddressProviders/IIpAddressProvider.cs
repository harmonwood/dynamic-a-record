namespace DynamicARecord.IpAddressProviders
{
    public interface IIpAddressProvider
    {
       Task<string> GetExternalIpAddressAsync(); 
       string Name();
    }
}