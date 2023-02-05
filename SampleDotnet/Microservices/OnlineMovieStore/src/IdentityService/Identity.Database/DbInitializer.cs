using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IDbContextFactory<IdentityDbContext> contextFactory)
        {
            using (var context = contextFactory.CreateRepository())
            {
                context.Database.EnsureCreated();

                if (!context.AsQueryable<UserEntity>().Any())
                {
                    var user1 = new UserEntity()
                    {
                        Username = "user1",
                        Password = "password1",
                        Email = "user1@test.com",
                        Name = "Falan",
                        Surname = "Filan",
                    };
                    context.Insert(user1);

                    var user2 = new UserEntity()
                    {
                        Username = "user2",
                        Password = "password2",
                        Email = "user2@test.com",
                        Name = "Alavere",
                        Surname = "Dalavere",
                    };
                    context.Insert(user2);

                    context.SaveChanges();
                }
            }
        }
    }
}