using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Flurl.Http.Testing;
using Flurl.Http;
using DynamicARecord.IpAddressProviders;

namespace DynamicARecord.Tests.IpAddressProviders
{
    public class IpInfoAddressProviderTests
    {
        [Fact]
        public async void GetExternalIpAddressAsync_ValidResponse_ReturnsIpAddress()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new { ip = "123.45.67.89" });

                var testingClass = new IpInfoAddressProvider();

                var ipAddress = await testingClass.GetExternalIpAddressAsync();

                Assert.Equal("123.45.67.89", ipAddress);
                httpTest.ShouldHaveCalled("https://ipinfo.io/").WithVerb(HttpMethod.Get);
            }
        }

        [Fact]
        public async void GetExternalIpAddressAsync_ErrorResponse_ReturnsNull()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith("Bad Request", 400);

                await Assert.ThrowsAsync<FlurlHttpException>(async () => 
                {
                    var testingClass = new IpInfoAddressProvider();
                    await testingClass.GetExternalIpAddressAsync();
                });
            
                httpTest.ShouldHaveCalled("https://ipinfo.io/").WithVerb(HttpMethod.Get);
            }
        }
    }
}