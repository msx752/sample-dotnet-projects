using CustomImageProvider.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace netCoreAPI.Tests.Controllers
{
    public class PersonalRestfulControllerTests : MainControllerTests
    {
        public PersonalRestfulControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("api/PersonalRestful/@number")]
        public async Task<PersonalDto> Delete(string url)
        {
            var obj = await Post("api/PersonalRestful");
            var response = await client.DeleteAsync(url.Replace("@number", obj.Id.ToString()));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalDto>(response);
            Assert.Equal(obj.Name, data.Name);
            Assert.Equal(obj.Surname, data.Surname);
            Assert.Equal(obj.Id, data.Id);
            return data;
        }

        [Theory]
        [InlineData("api/PersonalRestful")]
        public async Task GetAll(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<PersonalDto> data = await DeserializeObjAsync<List<PersonalDto>>(response);
        }

        [Theory]
        [InlineData("api/PersonalRestful/1")]
        public async Task GetById(string url)
        {
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = await DeserializeObjAsync<PersonalDto>(response);
            Assert.Equal(1, data.Id);
        }

        [Theory]
        [InlineData("api/PersonalRestful")]
        public async Task<PersonalDto> Post(string url)
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
            var data = await DeserializeObjAsync<PersonalDto>(response);
            Assert.Equal(obj.Name, data.Name);
            Assert.Equal(obj.Surname, data.Surname);
            return data;
        }

        [Theory]
        [InlineData("api/PersonalRestful/@number")]
        public async Task Put(string url)
        {
            PersonalDto obj = await Post("api/PersonalRestful");
            obj.NationalId = "9999999999";
            obj.Age = 25;
            var response = await client.PutAsync(url.Replace("@number", obj.Id.ToString()), new StringContent(JsonConvert.SerializeObject(obj, jsonSerializerSettings), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}