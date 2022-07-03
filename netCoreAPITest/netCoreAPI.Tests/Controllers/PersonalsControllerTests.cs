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
    public class PersonalsControllerTests : MainControllerTests
    {
        public PersonalsControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task<BaseResponseModel<PersonalDto>> Delete()
        {
            var obj = await Post();
            Assert.NotEmpty(obj.Result);
            Assert.Single(obj.Result);

            var response = await client.DeleteAsync($"api/Personals/{obj.Result.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Equal(obj.Result.First().Name, data.Result.First().Name);
            Assert.Equal(obj.Result.First().Surname, data.Result.First().Surname);
            Assert.Equal(obj.Result.First().Id, data.Result.First().Id);
            return data;
        }

        [Theory]
        [InlineData(1000000)]
        public async Task DeleteById_NotFound(int personalId)
        {
            var response = await client.DeleteAsync($"api/Personals/{personalId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Equal(HttpStatusCode.NotFound, data.StatusCode);
            Assert.Empty(data.Result);
        }

        [Fact]
        public async Task GetAll()
        {
            var response = await client.GetAsync("api/Personals");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetById(int personalId)
        {
            var response = await client.GetAsync($"api/Personals/{personalId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.Equal(1, data.Result.First().Id);
        }

        [Theory]
        [InlineData("Mehmet")]
        [InlineData("Ahmet")]
        public async Task GetByName(string personalName)
        {
            var response = await client.GetAsync($"api/Personals/Name/{personalName}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.NotEqual(0, data.Result.First().Id);
        }

        [Theory]
        [InlineData("SAVCI")]
        [InlineData("FILAN")]
        public async Task GetBySurname(string personalSurname)
        {
            var response = await client.GetAsync($"api/Personals/Surname/{personalSurname}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
            Assert.NotEqual(0, data.Result.First().Id);
        }

        [Fact]
        public async Task<BaseResponseModel<PersonalDto>> Post()
        {
            PersonalModel obj = new PersonalModel()
            {
                Age = 30,
                Name = "testName",
                NationalId = null,
                Surname = "testSurname"
            };
            var response = await client.PostAsync("api/Personals", new StringContent(JsonConvert.SerializeObject(obj, jsonSerializerSettings), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.Equal(obj.Name, data.Result.First().Name);
            Assert.Equal(obj.Surname, data.Result.First().Surname);
            return data;
        }

        [Fact]
        public async Task Put()
        {
            BaseResponseModel<PersonalDto> obj = await Post();

            PersonalModel objRequest = new PersonalModel()
            {
                Age = 25,
                Name = obj.Result.First().Name,
                NationalId = "9999999999",
                Surname = obj.Result.First().Surname
            };
            var response = await client.PutAsync($"api/Personals/{obj.Result.First().Id}", new StringContent(JsonConvert.SerializeObject(objRequest, jsonSerializerSettings), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("sa")]
        public async Task Search(string queryString)
        {
            var response = await client.GetAsync($"api/Personals/Search?q={queryString}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = await DeserializeObjAsync<BaseResponseModel<PersonalDto>>(response);
            Assert.NotEmpty(data.Result);
        }
    }
}