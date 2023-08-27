using DynamicARecord.DnsProviders;

namespace DynamicARecord.Tests.DnsProviders
{
    public class DnsProviderFactoryTests
    {
        [Fact]
        public void CreateProvider_ValidKey_ReturnsExpectedProvider()
        {
            string validKey = NameCheapDnsProvider.Name();
            string[] validArgs = { "apiUser", "apiKey", "clientIp" };

            var provider = DnsProviderFactory.CreateProvider(validKey, validArgs);

            Assert.IsType<NameCheapDnsProvider>(provider);
        }

        [Fact]
        public void CreateProvider_InvalidKey_ThrowsArgumentException()
        {
            string invalidKey = "InvalidProvider";
            string[] args = { "arg1", "arg2", "arg3" };

            Assert.Throws<ArgumentException>(() => DnsProviderFactory.CreateProvider(invalidKey, args));
        }
    }
}
