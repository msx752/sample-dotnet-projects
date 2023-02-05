using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using OnlineMovieStore.Tests.OcelotRedirections;
using SampleProject.Basket.API.Models.Dtos;
using SampleProject.Cart.API.Models.Dtos;
using SampleProject.Cart.API.Models.Requests;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Movie.API.Models.Responses;
using SampleProject.Result.Abstractions;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMovieStore.Tests.Integrations
{
    public class EndToEndIntegrationTests : MainControllerTests
    {
        public EndToEndIntegrationTests()
        {
            var strCon = new SqlConnectionStringBuilder();
            strCon.DataSource = "127.0.0.1:1433\\MovieDbContext";
            strCon.UserID = "su";
            strCon.Password = "Admin123";
            strCon.TrustServerCertificate = true;
            strCon.MultipleActiveResultSets = true;
            strCon.Authentication = SqlAuthenticationMethod.SqlPassword;
            strCon.PersistSecurityInfo = false;
            var str = strCon.ToString();
        }

        [Fact]
        public async Task IntegrationTest_via_OcelotGateway_1()
        {
            //firstly, run local rabbitmq server

            #region Starts In-Memory Microservices

            var movieService = new CustomWebApplicationFactory<SampleProject.Movie.API.Startup>();
            var movieClient = movieService.CreateOcelotClient(1030);

            var identityService = new CustomWebApplicationFactory<SampleProject.Identity.API.Startup>();
            var identityClient = identityService.CreateOcelotClient(1020);

            var cartService = new CustomWebApplicationFactory<SampleProject.Cart.API.Startup>();
            var cartClient = cartService.CreateOcelotClient(1050);

            var paymentService = new CustomWebApplicationFactory<SampleProject.Payment.API.Startup>();
            var pamynetClient = paymentService.CreateOcelotClient(1040);

            var gatewayService = new OcelotWebApplicationFactory<SampleProject.Gateway.API.Startup>(new HttpClient[] {
                movieClient, identityClient, cartClient, pamynetClient }
            );
            var client = gatewayService.CreateOcelotClient(1010);

            #endregion Starts In-Memory Microservices

            #region GET: Movies

            //GET: Movies
            var responseMovies = ConvertResponse<ResponseModel<MovieIndexViewModel>>(await client.GetAsync($"/gateway/movies"));
            responseMovies.Errors.ShouldBeNull();

            #endregion GET: Movies

            #region POST: User Login

            //POST: User Login
            var contentUserLogin = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", "user1"),
                new KeyValuePair<string, string>("password", "password1"),
                new KeyValuePair<string, string>("grant_type", "password"),
            });
            var responseUserLogin = ConvertResponse<ResponseModel<TokenDto>>(await client.PostAsync($"/gateway/token", contentUserLogin));
            responseUserLogin.Errors.ShouldBeNull();
            var access_token = responseUserLogin.Results.First().access_token;
            var loggedInUserId = responseUserLogin.Results.First().User.Id;

            #endregion POST: User Login

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            #region GET: User By Id

            //GET: User By Id
            var responseUserById = ConvertResponse<ResponseModel<UserDto>>(await client.GetAsync($"/gateway/users/{loggedInUserId}"));
            responseUserById.Errors.ShouldBeNull();

            #endregion GET: User By Id

            #region GET: Cart

            //GET: Cart
            var responseCart = ConvertResponse<ResponseModel<CartDto>>(await client.GetAsync($"/gateway/carts"));
            responseCart.Errors.ShouldBeNull();
            var currentCartId = responseCart.Results.First().Id;

            #endregion GET: Cart

            #region POST: Add Product to Card

            //POST: Add Product to Card
            var contentProducttoCard = new StringContent(JsonConvert.SerializeObject(new CartItemAddModel()
            {
                ProductDatabase = "movie",
                ProductId = "tt0133093"
            }), new MediaTypeHeaderValue("application/json"));
            var responseProducttoCard = ConvertResponse<ResponseModel<CartItemDto>>(await client.PostAsync($"/gateway/carts/{currentCartId}/item", contentProducttoCard));
            responseProducttoCard.Errors.ShouldBeNull();

            #endregion POST: Add Product to Card

            #region POST: Create Payment

            //POST: Create Payment
            var responseCreatePayment = ConvertResponse<ResponseModel<string>>(await client.PostAsync($"/gateway/payments/create/{currentCartId}", null));
            responseCreatePayment.Errors.ShouldBeNull();

            #endregion POST: Create Payment

            #region GET: Payment History

            //GET: Payment History
            var responsePaymentHistory = ConvertResponse<ResponseModel<UserDto>>(await client.GetAsync($"/gateway/payments/history"));
            responsePaymentHistory.Errors.ShouldBeNull();
            responsePaymentHistory.Results.Count.ShouldNotBe(0);

            #endregion GET: Payment History
        }
    }
}