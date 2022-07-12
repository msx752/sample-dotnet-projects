using CustomImageProvider.Tests;
using Microsoft.Extensions.DependencyInjection;
using Samp.Core.Interfaces.Repositories;
using Samp.Identity.API;
using Samp.Identity.Core.Migrations;
using Samp.Identity.Database.Entities;
using Shouldly;
using System;
using Xunit;

namespace Samp.Tests.DbContexts
{
    public class IdentityDbContextTests : MainControllerTests<Samp.Identity.API.Startup>
    {
        public IdentityDbContextTests(CustomWebApplicationFactory<Samp.Identity.API.Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void Add_Update_Delete_with_AuditLog_Success()
        {
            //## Scope: ADD
            //
            Guid userId_HttpRequestSession1 = Guid.NewGuid();
            ISharedRepository<IdentityDbContext> repo_scope1;
            UserEntity user_scope1 = new() { Username = Guid.NewGuid().ToString(), Password = "test1234" };
            RefreshTokenEntity refreshToken_scope1 = new RefreshTokenEntity() { RefreshToken = Guid.NewGuid().ToString() };

            using (var scope1 = _factory.Services.CreateScope())
            {
                repo_scope1 = scope1.ServiceProvider.GetRequiredService<ISharedRepository<IdentityDbContext>>();
                repo_scope1.Table<UserEntity>().Add(user_scope1);                                               //Add UserEntity
                repo_scope1.Commit(userId_HttpRequestSession1);                                                 //Commit UserEntity
                user_scope1.ShouldNotBeNull();
                user_scope1.CreatedBy.ShouldBe(userId_HttpRequestSession1);

                refreshToken_scope1.UserId = user_scope1.Id;
                user_scope1.RefreshTokens.Add(refreshToken_scope1);                                             //Add RefreshTokenEntity
                repo_scope1.Commit(userId_HttpRequestSession1);                                                 //Commit RefreshTokenEntity
                refreshToken_scope1.ShouldNotBeNull();
                refreshToken_scope1.CreatedBy.ShouldBe(userId_HttpRequestSession1);
                refreshToken_scope1.UserId.ShouldBe(user_scope1.Id);
            }

            //## Scope: UPDATE
            //
            Guid userId_HttpRequestSession2 = Guid.NewGuid();
            ISharedRepository<IdentityDbContext> repo_scope2;
            UserEntity user_scope2 = null;
            RefreshTokenEntity refreshToken_scope2 = null;
            using (var scope2 = _factory.Services.CreateScope())
            {
                repo_scope2 = scope2.ServiceProvider.GetRequiredService<ISharedRepository<IdentityDbContext>>();
                user_scope2 = repo_scope2.Table<UserEntity>().GetById(user_scope1.Id);                          //Select UserEntity
                user_scope2.ShouldNotBeNull();
                user_scope2.Id.ShouldBe(user_scope1.Id);

                user_scope2.Password = "4321test";                                                              //Update UserEntity
                repo_scope2.Commit(userId_HttpRequestSession2);                                                 //Commit UserEntity
                user_scope2.Password.ShouldNotBe(user_scope1.Password);
                user_scope2.CreatedAt.ShouldBe(user_scope1.CreatedAt);
                user_scope2.CreatedBy.ShouldBe(userId_HttpRequestSession1);
                user_scope2.UpdatedBy.ShouldNotBeNull();
                user_scope2.UpdatedBy.ShouldBe(userId_HttpRequestSession2);

                refreshToken_scope2 = repo_scope2.Table<RefreshTokenEntity>().GetById(refreshToken_scope1.Id);  //Select RefreshTokenEntity
                refreshToken_scope2.ShouldNotBeNull();
                refreshToken_scope2.CreatedAt.ShouldBe(refreshToken_scope1.CreatedAt);
                refreshToken_scope2.CreatedBy.ShouldBe(userId_HttpRequestSession1);
                refreshToken_scope2.UserId.ShouldBe(user_scope1.Id);
                refreshToken_scope2.RefreshToken.ShouldBe(refreshToken_scope1.RefreshToken);
                refreshToken_scope2.UpdatedBy.ShouldBeNull();
                refreshToken_scope2.UpdatedBy.ShouldBeNull();

                refreshToken_scope2.RefreshToken = Guid.NewGuid().ToString();                                   //Update RefreshTokenEntity
                repo_scope2.Commit(userId_HttpRequestSession2);                                                 //Commit RefreshTokenEntity
                refreshToken_scope2.UpdatedBy.ShouldNotBeNull();
                refreshToken_scope2.UpdatedBy.ShouldBe(userId_HttpRequestSession2);
                refreshToken_scope2.RefreshToken.ShouldNotBe(refreshToken_scope1.RefreshToken);
            }

            //## Scope: DELETE
            //
            Guid userId_HttpRequestSession3 = Guid.NewGuid();
            ISharedRepository<IdentityDbContext> repo_scope3;
            using (var scope3 = _factory.Services.CreateScope())
            {
                repo_scope3 = scope3.ServiceProvider.GetRequiredService<ISharedRepository<IdentityDbContext>>();
            }
        }
    }
}