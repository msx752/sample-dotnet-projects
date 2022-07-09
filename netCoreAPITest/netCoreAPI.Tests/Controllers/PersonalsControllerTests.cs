﻿using CustomImageProvider.Tests;
using netCoreAPI.Controllers.Requests;
using netCoreAPI.Core.Results.Abstracts;
using netCoreAPI.Models.Dtos;
using Newtonsoft.Json;
using Shouldly;
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
        public async Task<ResponseModel<PersonalDto>> Delete()
        {
            var obj = await Post();
            obj.Result.ShouldNotBeEmpty();
            obj.Result.Count.ShouldBeEquivalentTo(1);

            var response = await client.DeleteAsync($"api/Personals/{obj.Result.First().Id}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Errors.ShouldBeNull();
            data.Result.ShouldNotBeNull();
            data.Result.First().Name.ShouldBe(obj.Result.First().Name);
            data.Result.First().Surname.ShouldBe(obj.Result.First().Surname);
            data.Result.First().Id.ShouldBe(obj.Result.First().Id);

            return data;
        }

        [Theory]
        [InlineData(1000000)]
        public async Task DeleteById_NotFound(int personalId)
        {
            var response = await client.DeleteAsync($"api/Personals/{personalId}");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll()
        {
            var response = await client.GetAsync("api/Personals");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);

            data.Errors.ShouldBeNull();
            data.Result.ShouldNotBeNull();
            data.Result.Count.ShouldBeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetById(int personalId)
        {
            var response = await client.GetAsync($"api/Personals/{personalId}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);
            data.Result.ShouldNotBeEmpty();
            data.Result.First().Id.ShouldBe(1);
        }

        [Theory]
        [InlineData("Mehmet")]
        [InlineData("Ahmet")]
        public async Task GetByName(string personalName)
        {
            var response = await client.GetAsync($"api/Personals/Name/{personalName}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);
            data.Result.ShouldNotBeEmpty();
            data.Result.First().Id.ShouldNotBe(0);
        }

        [Theory]
        [InlineData("SAVCI")]
        [InlineData("FILAN")]
        public async Task GetBySurname(string personalSurname)
        {
            var response = await client.GetAsync($"api/Personals/Surname/{personalSurname}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);
            data.Result.ShouldNotBeEmpty();
            data.Result.First().Id.ShouldNotBe(0);
        }

        [Fact]
        public async Task<ResponseModel<PersonalDto>> Post()
        {
            PersonalRequest obj = new PersonalRequest()
            {
                Age = 30,
                Name = "testName",
                NationalId = null,
                Surname = "testSurname"
            };

            var response = await client.PostAsync("api/Personals",
                new StringContent(JsonConvert.SerializeObject(obj, jsonSerializerSettings), Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);
            data.Result.First().Name.ShouldBe(obj.Name);
            data.Result.First().Surname.ShouldBe(obj.Surname);

            return data;
        }

        [Fact]
        public async Task Put()
        {
            ResponseModel<PersonalDto> obj = await Post();

            PersonalRequest objRequest = new PersonalRequest()
            {
                Age = 25,
                Name = obj.Result.First().Name,
                NationalId = "9999999999",
                Surname = obj.Result.First().Surname
            };

            var response = await client.PutAsync($"api/Personals/{obj.Result.First().Id}",
                new StringContent(JsonConvert.SerializeObject(objRequest, jsonSerializerSettings), Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("sa")]
        public async Task Search(string queryString)
        {
            var response = await client.GetAsync($"api/Personals/Search?q={queryString}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var data = ConvertResponse<ResponseModel<PersonalDto>>(response);
            data.Result.ShouldNotBeEmpty();
        }
    }
}