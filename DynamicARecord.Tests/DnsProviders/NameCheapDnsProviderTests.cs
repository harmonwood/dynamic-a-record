using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Flurl.Http.Testing;
using DynamicARecord.DnsProviders;

namespace DynamicARecord.Tests.DnsProviders
{
    public class NameCheapDnsProviderTests
    {
        [Fact]
        public async Task UpdateDnsRecordAsync_ShouldSendCorrectRequest()
        {
            using var httpTest = new HttpTest();

            var apiUser = "testUser";
            var apiKey = "testKey";
            var clientIp = "192.168.1.1";
            var domain = "example.com";
            var subdomain = "sub";
            var ipAddress = "192.168.1.2";

            var provider = new NameCheapDnsProvider(apiUser, apiKey, clientIp);

            httpTest.ForCallsTo("https://api.namecheap.com/xml.response")
                .WithVerb(HttpMethod.Post)
                .WithQueryParam("ApiUser", apiUser)
                .WithQueryParam("ApiKey", apiKey)
                .WithQueryParam("UserName", apiUser)
                .WithQueryParam("ClientIp", clientIp)
                .WithQueryParam("Command", "namecheap.domains.dns.setHosts")
                .WithQueryParam("SLD", "example")
                .WithQueryParam("TLD", "com")
                .WithQueryParam("HostName1", subdomain)
                .WithQueryParam("RecordType1", "A")
                .WithQueryParam("Address1", ipAddress)
                .RespondWith("OK", 200);

            var result = await provider.UpdateDnsRecordAsync(domain, subdomain, ipAddress);

            Assert.True(result);
            httpTest.ShouldHaveCalled("https://api.namecheap.com/xml.response")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }
    }
}