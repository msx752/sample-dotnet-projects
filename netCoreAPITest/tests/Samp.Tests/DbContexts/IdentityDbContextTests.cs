using CustomImageProvider.Tests;
using Microsoft.Extensions.DependencyInjection;
using Samp.Core.Entities;
using Samp.Core.Interfaces.Repositories;
using Samp.Identity.API;
using Samp.Identity.Core.Migrations;
using Samp.Identity.Database.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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
            UserEntity user_scope1 = new() { Username = Guid.NewGuid().ToString(), Password = "test1234" };
            RefreshTokenEntity refreshToken_scope1 = new RefreshTokenEntity() { RefreshToken = Guid.NewGuid().ToString() };

            using (var scope1 = _factory.Services.CreateScope())
            {
                var repo_scope1 = scope1.ServiceProvider
                    .GetRequiredService<ISharedRepository<IdentityDbContext>>();
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
            UserEntity user_scope2 = null;
            RefreshTokenEntity refreshToken_scope2 = null;
            using (var scope2 = _factory.Services.CreateScope())
            {
                var repo_scope2 = scope2.ServiceProvider
                    .GetRequiredService<ISharedRepository<IdentityDbContext>>();
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
            UserEntity user_scope3 = null;
            using (var scope3 = _factory.Services.CreateScope())
            {
                var repo_scope3 = scope3.ServiceProvider
                    .GetRequiredService<ISharedRepository<IdentityDbContext>>();
                user_scope3 = repo_scope3.Table<UserEntity>().GetById(user_scope2.Id);                          //Select UserEntity
                user_scope3.IsActive.ShouldBeTrue();
                user_scope3.UpdatedBy.ShouldBe(user_scope3.UpdatedBy);
                user_scope3.UpdatedAt.ShouldBe(user_scope3.UpdatedAt);

                repo_scope3.Table<UserEntity>().Delete(user_scope3);                                            //Delete UserEntity
                repo_scope3.Commit(userId_HttpRequestSession3);                                                 //Commit UserEntity
                user_scope3.IsActive.ShouldBeFalse();
                user_scope3.UpdatedBy.ShouldNotBe(user_scope2.UpdatedBy);
                user_scope3.UpdatedAt.ShouldNotBe(user_scope2.UpdatedAt);
                user_scope3.UpdatedBy.ShouldBe(userId_HttpRequestSession3);
            }

            Guid userId_HttpRequestSession4 = Guid.NewGuid();
            RefreshTokenEntity refreshToken_scope4 = null;
            using (var scope4 = _factory.Services.CreateScope())
            {
                var repo_scope4 = scope4.ServiceProvider
                    .GetRequiredService<ISharedRepository<IdentityDbContext>>();
                refreshToken_scope4 = repo_scope4.Table<RefreshTokenEntity>().GetById(refreshToken_scope2.Id);  //Select UserEntity
                refreshToken_scope4.IsActive.ShouldBeTrue();
                refreshToken_scope4.UpdatedBy.ShouldBe(refreshToken_scope2.UpdatedBy);
                refreshToken_scope4.UpdatedAt.ShouldBe(refreshToken_scope2.UpdatedAt);

                repo_scope4.Table<RefreshTokenEntity>().Delete(refreshToken_scope4);                            //Delete UserEntity
                repo_scope4.Commit(userId_HttpRequestSession4);                                                 //Commit UserEntity
                refreshToken_scope4.IsActive.ShouldBeFalse();
                refreshToken_scope4.UpdatedBy.ShouldNotBe(refreshToken_scope2.UpdatedBy);
                refreshToken_scope4.UpdatedAt.ShouldNotBe(refreshToken_scope2.UpdatedAt);
                refreshToken_scope4.UpdatedBy.ShouldBe(userId_HttpRequestSession4);
            }

            //## Scope: AUDIT LOGS
            //
            List<AuditEntity> auditLogs_scope5;
            using (var scope5 = _factory.Services.CreateScope())
            {
                var repo_scope5 = scope5.ServiceProvider
                    .GetRequiredService<ISharedRepository<IdentityDbContext>>();
                auditLogs_scope5 = repo_scope5.Table<AuditEntity>().All().OrderBy(f => f.CreatedAt).ToList();
            }

            //## VALIDATION OF THE AUDIT LOGS
            //validate it
        }
    }
}