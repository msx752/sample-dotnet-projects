using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IdentityDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    var user1 = new UserEntity()
                    {
                        Username = "user1",
                        Password = "password1",
                        Email = "user1@test.com",
                        Name = "Falan",
                        Surname = "Filan",
                    };
                    context.Add(user1);

                    var user2 = new UserEntity()
                    {
                        Username = "user2",
                        Password = "password2",
                        Email = "user2@test.com",
                        Name = "Alavere",
                        Surname = "Dalavere",
                    };
                    context.Add(user2);
                }

                context.SaveChanges();
            }
        }
    }
}