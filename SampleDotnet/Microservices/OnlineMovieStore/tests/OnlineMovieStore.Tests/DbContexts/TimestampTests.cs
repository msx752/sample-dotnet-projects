using Identity.Database;
using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleDotnet.RepositoryFactory.Interfaces;
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

            using (var scope = _factory.Services.CreateScope())
            using (var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>())
            using (var repository = unitOfWork.CreateRepository<IdentityDbContext>())
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
                await repository.InsertAsync(user1);

                await unitOfWork.SaveChangesAsync();

                user1.CreatedAt.ShouldNotBeNull();
                user1.UpdatedAt.ShouldBeNull();
                repository.Update(user1);

                await unitOfWork.SaveChangesAsync();

                user1.UpdatedAt.ShouldNotBeNull();
            }
        }
    }
}