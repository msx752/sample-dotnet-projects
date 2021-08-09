using Microsoft.AspNetCore.Mvc.Testing;
using netCoreAPI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CustomImageProvider.Tests
{
    public class MainControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        internal readonly CustomWebApplicationFactory<Startup> _factory;

        internal readonly HttpClient client = null;

        /// be sure same value with below of the pecified configuration, to not engage with type of any serialization error
        /// <seealso cref="namespace:netCoreAPI.Startup.ConfigureServices().AddNewtonsoftJson(settings)"/>
        internal readonly JsonSerializerSettings jsonSerializerSettings;

        public MainControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
            };
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
                    var resp = await sr.ReadToEndAsync();
                    var data = JsonConvert.DeserializeObject<T>(resp, jsonSerializerSettings);
                    return data;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}