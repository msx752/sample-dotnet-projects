using CustomImageProvider.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using netCoreAPI.Model.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace netCoreAPI.Tests.Controllers
{
    public class PersonalCustomControllerTests : MainControllerTests
    {
        public PersonalCustomControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("api/PersonalCustom/All")]
        public async Task All(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<List<PersonalViewModel>>(response);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/1000000")]
        public async Task DeleteById_NotFound(string url)
        {
            var response = await client.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalViewModel>(response);
            Assert.Equal(0, data.Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/id/1")]
        public async Task GetById(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalViewModel>(response);
            Assert.NotEqual(0, data.Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Name/Mustafa Salih")]
        public async Task GetByName(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalViewModel>(response);
            Assert.NotEqual(0, data.Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Surname/AVCI")]
        public async Task GetBySurname(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalViewModel>(response);
            Assert.NotEqual(0, data.Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Search?q=sa")]
        public async Task Search(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<List<PersonalViewModel>>(response);
            Assert.NotNull(data);
        }
    }
}