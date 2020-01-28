using CustomImageProvider.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using netCoreAPITest.Data.Tables;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace netCoreAPITest.Tests.Controllers
{
    public class PersonalCustomContollerTests : CustomControllerTests
    {
        public PersonalCustomContollerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("api/PersonalCustom/All")]
        public async Task All(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<Personal> data = await DeserializeObjAsync<List<Personal>>(response);
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/id/1")]
        public async Task GetById(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Personal data = await DeserializeObjAsync<Personal>(response);

            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Name/Mustafa Salih")]
        public async Task GetByName(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Personal data = await DeserializeObjAsync<Personal>(response);

            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Surname/AVCI")]
        public async Task GetBySurname(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Personal data = await DeserializeObjAsync<Personal>(response);

            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Search?q=sa")]
        public async Task Search(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<List<Personal>>(response);

            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }
    }
}