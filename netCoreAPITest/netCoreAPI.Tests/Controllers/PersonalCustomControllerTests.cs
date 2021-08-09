using CustomImageProvider.Tests;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.ResponseModels;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace netCoreAPI.Tests.Controllers
{
    public class PersonalCustomControllerTests : MainControllerTests
    {
        public PersonalCustomControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("api/PersonalCustom/All")]
        public async Task All(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("api/PersonalCustom/1000000")]
        public async Task DeleteById_NotFound(string url)
        {
            var response = await client.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Empty(data.Result);
        }

        [Theory]
        [InlineData("api/PersonalCustom/id/1")]
        public async Task GetById(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.NotEqual(0, data.Result.First().Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Name/Mustafa Salih")]
        public async Task GetByName(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.NotEqual(0, data.Result.First().Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Surname/SAVCI")]
        public async Task GetBySurname(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.NotEqual(0, data.Result.First().Id);
        }

        [Theory]
        [InlineData("api/PersonalCustom/Search?q=sa")]
        public async Task Search(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
        }
    }
}