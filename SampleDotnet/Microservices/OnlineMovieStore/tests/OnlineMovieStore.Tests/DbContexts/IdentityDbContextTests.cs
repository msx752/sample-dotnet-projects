﻿//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using OnlineMovieStore.Tests;
//using SampleProject.Core.Entities;
//using SampleProject.Core.Interfaces.Repositories;
//using SampleProject.Identity.Core.Migrations;
//using SampleProject.Identity.Database.Entities;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace OnlineMovieStore.Tests.DbContexts
//{
//    public class IdentityDbContextTests
//    {
//        public IdentityDbContextTests()
//        {
//        }

//        /// <summary>
//        /// run this test alone, otherwise you will get an error,
//        /// due to non-transactional in-memory database is not working well with WebApplicationFactory
//        /// </summary>
//        [Fact]
//        public void RUN_THIS_TEST_ALONE___Add_Update_Delete_with_AuditLog_Success()
//        {
//            CustomWebApplicationFactory<SampleProject.Identity.API.Startup> _factory = new CustomWebApplicationFactory<SampleProject.Identity.API.Startup>();
//            _factory.CreateClient();

//            //## Service Scope: DELETE Seed's AuditLogs
//            Guid userId_HttpRequestSession0 = Guid.NewGuid();
//            using (var scope0 = _factory.Services.CreateScope())
//            {
//                var repo_scope0 = scope0.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                var auditLogs_scope0 = repo_scope0.Table<AuditEntity>().AsQueryable();
//                repo_scope0.Table<AuditEntity>().Delete(auditLogs_scope0);
//                repo_scope0.SaveChanges();
//            }

//            Dictionary<string, int> auditlogCounter = new()
//            {
//                {nameof(UserEntity),0 },
//                {nameof(RefreshTokenEntity),0 }
//            };

//            //## Service Scope: ADD
//            //
//            Guid userId_HttpRequestSession1 = Guid.NewGuid();
//            UserEntity user_scope1 = new()
//            {
//                Username = Guid.NewGuid().ToString(),
//                Password = "test1234",
//                Email = "auditlog@test.com",
//                Name = "asdf",
//                Surname = "ghjk",
//            };
//            RefreshTokenEntity refreshToken_scope1 = new RefreshTokenEntity() { RefreshToken = Guid.NewGuid().ToString() };

//            using (var scope1 = _factory.Services.CreateScope())
//            {
//                var repo_scope1 = scope1.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                repo_scope1.Table<UserEntity>().Insert(user_scope1);                                            //Add UserEntity
//                repo_scope1.SaveChanges();                                                 //Commit UserEntity
//                auditlogCounter[nameof(UserEntity)] += 1;
//                user_scope1.ShouldNotBeNull();

//                refreshToken_scope1.UserId = user_scope1.Id;
//                user_scope1.RefreshTokens.Add(refreshToken_scope1);                                             //Add RefreshTokenEntity
//                repo_scope1.SaveChanges();                                                 //Commit UserEntity
//                auditlogCounter[nameof(RefreshTokenEntity)] += 1;
//                refreshToken_scope1.ShouldNotBeNull();
//                refreshToken_scope1.UserId.ShouldBe(user_scope1.Id);
//            }

//            //## Service Scope: UPDATE
//            //
//            Guid userId_HttpRequestSession2 = Guid.NewGuid();
//            UserEntity user_scope2 = null;
//            RefreshTokenEntity refreshToken_scope2 = null;
//            using (var scope2 = _factory.Services.CreateScope())
//            {
//                var repo_scope2 = scope2.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                user_scope2 = repo_scope2.Table<UserEntity>().GetById(user_scope1.Id);                          //Select UserEntity
//                user_scope2.ShouldNotBeNull();
//                user_scope2.Id.ShouldBe(user_scope1.Id);

//                user_scope2.Password = "4321test";                                                              //Update UserEntity
//                repo_scope2.SaveChanges();                                                 //Commit UserEntity
//                auditlogCounter[nameof(UserEntity)] += 1;
//                user_scope2.Password.ShouldNotBe(user_scope1.Password);
//                user_scope2.CreatedAt.ShouldBe(user_scope1.CreatedAt);

//                refreshToken_scope2 = repo_scope2.Table<RefreshTokenEntity>().GetById(refreshToken_scope1.Id);  //Select RefreshTokenEntity
//                refreshToken_scope2.ShouldNotBeNull();
//                refreshToken_scope2.CreatedAt.ShouldBe(refreshToken_scope1.CreatedAt);
//                refreshToken_scope2.UserId.ShouldBe(user_scope1.Id);
//                refreshToken_scope2.RefreshToken.ShouldBe(refreshToken_scope1.RefreshToken);

//                refreshToken_scope2.RefreshToken = Guid.NewGuid().ToString();                                   //Update RefreshTokenEntity
//                repo_scope2.SaveChanges();                                                 //Commit RefreshTokenEntity
//                auditlogCounter[nameof(RefreshTokenEntity)] += 1;
//                refreshToken_scope2.RefreshToken.ShouldNotBe(refreshToken_scope1.RefreshToken);
//            }

//            //## Service Scope: DELETE
//            //
//            Guid userId_HttpRequestSession3 = Guid.NewGuid();
//            UserEntity user_scope3 = null;
//            using (var scope3 = _factory.Services.CreateScope())
//            {
//                var repo_scope3 = scope3.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                user_scope3 = repo_scope3.Table<UserEntity>().GetById(user_scope2.Id);                          //Select UserEntity
//                user_scope3.UpdatedAt.ShouldBe(user_scope3.UpdatedAt);

//                repo_scope3.Table<UserEntity>().Delete(user_scope3);                                            //Delete UserEntity
//                repo_scope3.SaveChanges();                                                 //Commit UserEntity
//                auditlogCounter[nameof(UserEntity)] += 1;
//                user_scope3.UpdatedAt.ShouldNotBe(user_scope2.UpdatedAt);
//            }

//            Guid userId_HttpRequestSession4 = Guid.NewGuid();
//            RefreshTokenEntity refreshToken_scope4 = null;
//            using (var scope4 = _factory.Services.CreateScope())
//            {
//                var repo_scope4 = scope4.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                refreshToken_scope4 = repo_scope4.Table<RefreshTokenEntity>().GetById(refreshToken_scope2.Id);  //Select UserEntity
//                refreshToken_scope4.UpdatedAt.ShouldBe(refreshToken_scope2.UpdatedAt);

//                repo_scope4.Table<RefreshTokenEntity>().Delete(refreshToken_scope4);                            //Delete UserEntity
//                repo_scope4.SaveChanges();                                                 //Commit UserEntity
//                auditlogCounter[nameof(RefreshTokenEntity)] += 1;
//                refreshToken_scope4.UpdatedAt.ShouldNotBe(refreshToken_scope2.UpdatedAt);
//            }

//            //## Service Scope: AUDIT LOGS
//            //
//            List<AuditEntity> auditLogs_scope5;
//            using (var scope5 = _factory.Services.CreateScope())
//            {
//                var repo_scope5 = scope5.ServiceProvider
//                    .GetRequiredService<IUnitOfWork<IdentityDbContext>>();
//                auditLogs_scope5 = repo_scope5.Table<AuditEntity>().AsQueryable().OrderBy(f => f.CreatedAt).ToList();
//            }

//            //## VALIDATION OF THE AUDIT LOGS
//            var totalAuditCount = auditlogCounter.Select(f => f.Value).Sum();
//            auditLogs_scope5.Count.ShouldBe(totalAuditCount);
//            var enumerator = auditLogs_scope5.GetEnumerator();
//            enumerator.MoveNext(); //SERVICE SCOPE 1
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(UserEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(user_scope1.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Create);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.OldValues.ShouldBeEmpty();
//            enumerator.Current.AffectedColumns.ShouldBeEmpty();
//            enumerator.Current.NewValues.ShouldNotContain(TokenJsonString(new { UserId = user_scope1.Id }));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { user_scope1.Username }));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { user_scope1.Password }));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { user_scope1.CreatedAt }));

//            enumerator.MoveNext(); //SERVICE SCOPE 1
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(RefreshTokenEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(refreshToken_scope1.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Create);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.AffectedColumns.ShouldBeEmpty();
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { UserId = user_scope1.Id }));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { refreshToken_scope1.RefreshToken }));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { refreshToken_scope1.CreatedAt }));

//            enumerator.MoveNext(); //SERVICE SCOPE 2
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(UserEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(user_scope2.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Update);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.PrimaryKey.ShouldContain(TokenJsonString(new { user_scope2.Id }));
//            enumerator.Current.AffectedColumns.ShouldNotBeNull();
//            enumerator.Current.AffectedColumns.ShouldContain(nameof(user_scope2.Password));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { user_scope2.Password }));
//            enumerator.Current.OldValues.ShouldContain(TokenJsonString(new { user_scope1.Password }));

//            enumerator.MoveNext(); //SERVICE SCOPE 2
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(RefreshTokenEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(refreshToken_scope2.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Update);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.PrimaryKey.ShouldContain(TokenJsonString(new { refreshToken_scope2.Id }));
//            enumerator.Current.AffectedColumns.ShouldNotBeNull();
//            enumerator.Current.AffectedColumns.ShouldContain(nameof(refreshToken_scope2.RefreshToken));
//            enumerator.Current.NewValues.ShouldContain(TokenJsonString(new { refreshToken_scope2.RefreshToken }));
//            enumerator.Current.OldValues.ShouldContain(TokenJsonString(new { refreshToken_scope1.RefreshToken }));

//            enumerator.MoveNext(); //SERVICE SCOPE 3
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(UserEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(user_scope3.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Delete);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.PrimaryKey.ShouldContain(TokenJsonString(new { user_scope3.Id }));
//            enumerator.Current.AffectedColumns.ShouldNotBeNull();

//            enumerator.MoveNext(); //SERVICE SCOPE 4
//            enumerator.Current.Identifier.ShouldBe(Guid.Empty.ToString());
//            enumerator.Current.TableName.ShouldBe(nameof(RefreshTokenEntity));
//            enumerator.Current.CreatedAt.ToString().ShouldBe(refreshToken_scope4.CreatedAt.ToString());
//            enumerator.Current.Type.ShouldBe(SampleProject.Core.Enums.AuditType.Delete);
//            enumerator.Current.UpdatedAt.ShouldBeNull();
//            enumerator.Current.PrimaryKey.ShouldContain(TokenJsonString(new { refreshToken_scope4.Id }));
//            enumerator.Current.AffectedColumns.ShouldNotBeNull();

//            enumerator.MoveNext();
//            enumerator.Current.ShouldBeNull();
//        }

//        private string TokenJsonString(object obj)
//        {
//            var json = JsonConvert.SerializeObject(obj);

//            return json.Trim(' ').Substring(1, json.Length - 2);
//        }
//    }
//}