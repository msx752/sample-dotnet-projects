using Cart.Database;
using Identity.Database;
using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMovieStore.Tests.DbContexts
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/events#example-timestamp-state-changes
    /// </summary>
    public class TimestampTests
    {
        [Fact]
        public async Task CreatedUpdatedDeletedTimestamps_Tests()
        {
            CustomWebApplicationFactory<SampleProject.Identity.API.Startup> _factory = new CustomWebApplicationFactory<SampleProject.Identity.API.Startup>();
            _factory.CreateClient();

            using (var dbContext = await _factory.Services.GetRequiredService<IDbContextFactory<IdentityDbContext>>().CreateDbContextAsync())
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
                await dbContext.AddAsync(user1);

                await dbContext.SaveChangesAsync();

                user1.CreatedAt.ShouldNotBeNull();
                user1.UpdatedAt.ShouldBeNull();
                dbContext.Update(user1);

                await dbContext.SaveChangesAsync();

                user1.UpdatedAt.ShouldNotBeNull();
            }
        }
    }
}