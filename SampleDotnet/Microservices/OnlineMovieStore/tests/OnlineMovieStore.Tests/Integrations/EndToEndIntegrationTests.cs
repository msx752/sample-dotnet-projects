using CustomImageProvider.Tests;
using Newtonsoft.Json;
using Samp.Basket.API.Models.Dtos;
using Samp.Cart.API.Models.Dtos;
using Samp.Cart.API.Models.Requests;
using Samp.Identity.API.Models.Dto;
using Samp.Identity.API.Models.Requests;
using Samp.Movie.API.Models.Responses;
using Samp.Result;
using Samp.Result.Abstractions;
using Samp.Tests.OcelotRedirections;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Samp.Tests.Integrations
{
    public class EndToEndIntegrationTests : MainControllerTests
    {
        [Fact]
        public async Task IntegrationTest_via_OcelotGateway_1()
        {
            //firstly, run local rabbitmq server

            #region Starts In-Memory Microservices

            var movieService = new CustomWebApplicationFactory<Movie.API.Startup>();
            var movieClient = movieService.CreateOcelotClient(1030);

            var identityService = new CustomWebApplicationFactory<Identity.API.Startup>();
            var identityClient = identityService.CreateOcelotClient(1020);

            var cartService = new CustomWebApplicationFactory<Cart.API.Startup>();
            var cartClient = cartService.CreateOcelotClient(1050);

            var paymentService = new CustomWebApplicationFactory<Payment.API.Startup>();
            var pamynetClient = paymentService.CreateOcelotClient(1040);

            var gatewayService = new OcelotWebApplicationFactory<Gateway.API.Startup>(new HttpClient[] {
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
            responsePaymentHistory.Results.Count.ShouldBe(1);

            #endregion GET: Payment History
        }
    }
}