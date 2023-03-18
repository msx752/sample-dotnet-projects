using Identity.Database;
using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace OnlineMovieStore.Tests.DbContexts
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/events#example-timestamp-state-changes
    /// </summary>
    public class TimestampTests
    {
        [Fact]
        public void CreatedUpdatedDeletedTimestamps_Tests()
        {
            CustomWebApplicationFactory<SampleProject.Identity.API.Startup> _factory = new CustomWebApplicationFactory<SampleProject.Identity.API.Startup>();
            _factory.CreateClient();

            using (var scope = _factory.Services.CreateScope())
            using (var repository = scope.ServiceProvider.GetRequiredService<IDbContextFactory<IdentityDbContext>>().CreateRepository())
            {
                var user1 = new UserEntity()
                {
                    Username = "created",
                    Password = "time",
                    Email = "created@at.com",
                    Name = "Created",
                    Surname = "Time",
                };

                user1.CreatedAt.ShouldBeNull();
                repository.Insert(user1);
                repository.SaveChanges();
                user1.CreatedAt.ShouldNotBeNull();

                user1.UpdatedAt.ShouldBeNull();
                repository.Update(user1);
                repository.SaveChanges();
                user1.UpdatedAt.ShouldNotBeNull();
            }
        }
    }
}