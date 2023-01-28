using SampleProject.Core.Database;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Identity.Core.Migrations;
using SampleProject.Identity.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Auth.Database
{
    public class IdentityContextSeed : ContextSeed<IdentityDbContext>
    {
        public IdentityContextSeed(IUnitOfWork<IdentityDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
            if (Repository.Table<UserEntity>().AsQueryable().Count() == 0)
            {
                var user1 = new UserEntity()
                {
                    Username = "user1",
                    Password = "password1",
                    Email = "user1@test.com",
                    Name = "Falan",
                    Surname = "Filan",
                };

                Repository.Table<UserEntity>().Insert(user1);

                var user2 = new UserEntity()
                {
                    Username = "user2",
                    Password = "password2",
                    Email = "user2@test.com",
                    Name = "Alavere",
                    Surname = "Dalavere",
                };

                Repository.Table<UserEntity>().Insert(user2);

                Repository.SaveChanges(Guid.NewGuid());
            }
        }
    }
}