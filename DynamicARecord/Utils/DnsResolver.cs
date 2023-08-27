using System.Net.Sockets;
using System.Net;

public class DnsResolver
{
    public static IPAddress[]? GetIpAddress(string fqdn)
    {
        try
        {
            var addresses = Dns.GetHostAddresses(fqdn);
            return addresses;
        }
        catch (Exception)
        {
            return new IPAddress[0];
        }
    }

    public static bool validIpAddress(string fqdn, string ipAddress)
    {
        var addresses = GetIpAddress(fqdn);

        if (addresses == null) return false;

        return addresses.Any(address => address.ToString() == ipAddress);
    }
}
