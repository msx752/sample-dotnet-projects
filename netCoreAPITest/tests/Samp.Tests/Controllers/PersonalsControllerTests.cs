using CustomImageProvider.Tests;
using Newtonsoft.Json;
using Samp.API.Personal.Models.Dtos;
using Samp.API.Personal.Models.Requests;
using Samp.Core.Results.Abstracts;
using Shouldly;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Samp.Tests.Controllers
{
    public class PersonalsControllerTests : MainControllerTests<API.Personal.Startup>
    {
        public PersonalsControllerTests(CustomWebApplicationFactory<API.Personal.Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task<ResponseModel<PersonalDto>> Delete()
        {
            var obj = await Post();
            obj.Results.ShouldNotBeEmpty();
            obj.Results.Count.ShouldBeEquivalentTo(1);

            var response = await client.DeleteAsync($"api/Personals/{obj.Results.First().Id}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Errors.ShouldBeNull();
            data.Results.ShouldNotBeNull();
            data.Results.First().Name.ShouldBe(obj.Results.First().Name);
            data.Results.First().Surname.ShouldBe(obj.Results.First().Surname);
            data.Results.First().Id.ShouldBe(obj.Results.First().Id);

            return data;
        }

        [Theory]
        [InlineData(1000000)]
        public async Task DeleteById_NotFound(int personalId)
        {
            var response = await client.DeleteAsync($"api/Personals/{personalId}");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.ShouldBeNull();
            data.Errors.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll()
        {
            var response = await client.GetAsync("api/Personals");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Errors.ShouldBeNull();
            data.Results.ShouldNotBeNull();
            data.Results.Count.ShouldBeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetById(int personalId)
        {
            var response = await client.GetAsync($"api/Personals/{personalId}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.ShouldNotBeEmpty();
            data.Results.First().Id.ShouldBe(1);
        }

        [Theory]
        [InlineData("Fuat")]
        [InlineData("Ahmet")]
        public async Task GetByName(string personalName)
        {
            var response = await client.GetAsync($"api/Personals/Name/{personalName}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.ShouldNotBeEmpty();
            data.Results.First().Id.ShouldNotBe(0);
        }

        [Theory]
        [InlineData("MUAT")]
        [InlineData("FILAN")]
        public async Task GetBySurname(string personalSurname)
        {
            var response = await client.GetAsync($"api/Personals/Surname/{personalSurname}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.ShouldNotBeEmpty();
            data.Results.First().Id.ShouldNotBe(0);
        }

        [Fact]
        public async Task<ResponseModel<PersonalDto>> Post()
        {
            PersonalModel obj = new PersonalModel()
            {
                Age = 30,
                Name = "testName",
                NationalId = "123456789",
                Surname = "testSurname"
            };

            var response = await client.PostAsync("api/Personals",
                new StringContent(JsonConvert.SerializeObject(obj, jsonSerializerSettings), Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.First().Name.ShouldBe(obj.Name);
            data.Results.First().Surname.ShouldBe(obj.Surname);

            return data;
        }

        [Fact]
        public async Task Put()
        {
            ResponseModel<PersonalDto> obj = await Post();

            PersonalModel objRequest = new PersonalModel()
            {
                Age = 25,
                Name = obj.Results.First().Name,
                NationalId = "9999999999",
                Surname = obj.Results.First().Surname
            };

            var response = await client.PutAsync($"api/Personals/{obj.Results.First().Id}",
                new StringContent(JsonConvert.SerializeObject(objRequest, jsonSerializerSettings), Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("a")]
        public async Task Search(string queryString)
        {
            var response = await client.GetAsync($"api/Personals/Search?q={queryString}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Stats.RId.ShouldNotBeNull();
            data.Results.ShouldNotBeEmpty();
        }
    }
}