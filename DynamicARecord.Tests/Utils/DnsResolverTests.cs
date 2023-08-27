using System;
using System.Linq;
using Xunit;
using Moq;

namespace DynamicARecord.Tests.Utils
{
    public class DnsResolverTests
    {
        [Fact]
        public void GetIpAddress_WithValidFqdn_ReturnsAddresses()
        {
            // Setup
            var fqdn = "example.com";

            // Act
            var result = DnsResolver.GetIpAddress(fqdn);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public void GetIpAddress_WithInvalidFqdn_ReturnsNull()
        {
            var fqdn = "invalid-domain";

            var result = DnsResolver.GetIpAddress(fqdn);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidIpAddress_WithMatchingIpAddress_ReturnsTrue()
        {
            var fqdn = "example.com";
            var ipAddress = "93.184.216.34"; // Assuming this IP is valid for example.com 

            var isValid = DnsResolver.validIpAddress(fqdn, ipAddress);

            Assert.True(isValid);
        }

        [Fact]
        public void ValidIpAddress_WithNonMatchingIpAddress_ReturnsFalse()
        {
            var fqdn = "example.com";
            var ipAddress = "192.168.1.1"; // This IP should not be valid for example.com 

            var isValid = DnsResolver.validIpAddress(fqdn, ipAddress);

            Assert.False(isValid);
        }
    }
}
