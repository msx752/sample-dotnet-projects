using Microsoft.AspNetCore.Mvc.Testing;
using netCoreAPITest;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CustomImageProvider.Tests
{
    public class CustomControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        internal readonly WebApplicationFactory<Startup> _factory;
        internal readonly HttpClient client = null;
        public CustomControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            // Arrange
            client = _factory.CreateClient();
        }

        internal async Task<T> DeserializeObjAsync<T>(HttpResponseMessage response) where T : class
        {
            using (var sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<T>(await sr.ReadToEndAsync());
                    Assert.True(true, "succeed");
                    return data;
                }
                catch (Exception e)
                {
                    Assert.True(false, e.Message);
                    return null;
                }
            }
        }
    }
}