using CustomImageProvider.Tests;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.Models;
using netCoreAPI.Model.ResponseModels;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace netCoreAPI.Tests.Controllers
{
    public class PersonalRestfulControllerTests : MainControllerTests
    {
        public PersonalRestfulControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("api/PersonalRestful/@number")]
        public async Task<BaseResponseModel<PersonalDto>> Delete(string url)
        {
            var obj = await Post("api/PersonalRestful");
            Assert.NotEmpty(obj.Result);
            Assert.Single(obj.Result);
            var response = await client.DeleteAsync(url.Replace("@number", obj.Result.First().Id.ToString()));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Equal(obj.Result.First().Name, data.Result.First().Name);
            Assert.Equal(obj.Result.First().Surname, data.Result.First().Surname);
            Assert.Equal(obj.Result.First().Id, data.Result.First().Id);
            return data;
        }

        [Theory]
        [InlineData("api/PersonalRestful")]
        public async Task GetAll(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
        }

        [Theory]
        [InlineData("api/PersonalRestful/1")]
        public async Task GetById(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.Equal(1, data.Result.First().Id);
        }

        [Theory]
        [InlineData("api/PersonalRestful")]
        public async Task<BaseResponseModel<PersonalDto>> Post(string url)
        {
            PersonalModel obj = new PersonalModel()
            {
                Age = 30,
                Name = "testName",
                NationalId = null,
                Surname = "testSurname"
            };
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj, jsonSerializerSettings), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Equal(obj.Name, data.Result.First().Name);
            Assert.Equal(obj.Surname, data.Result.First().Surname);
            return data;
        }

        [Theory]
        [InlineData("api/PersonalRestful/@number")]
        public async Task Put(string url)
        {
            BaseResponseModel<PersonalDto> obj = await Post("api/PersonalRestful");

            PersonalModel objRequest = new PersonalModel()
            {
                Age = 25,
                Name = obj.Result.First().Name,
                NationalId = "9999999999",
                Surname = obj.Result.First().Surname
            };
            var response = await client.PutAsync(url.Replace("@number", obj.Result.First().Id.ToString()), new StringContent(JsonConvert.SerializeObject(objRequest, jsonSerializerSettings), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}