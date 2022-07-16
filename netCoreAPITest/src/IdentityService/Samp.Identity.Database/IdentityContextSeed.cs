using Samp.Core.Database;
using Samp.Core.Interfaces.Repositories;
using Samp.Identity.Core.Migrations;
using Samp.Identity.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Auth.Database
{
    public class IdentityContextSeed : ContextSeed<IdentityDbContext>
    {
        public IdentityContextSeed(ISharedRepository<IdentityDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
            var user1 = new UserEntity()
            {
                Username = "mustafa",
                Password = "1234"
            };

            Repository.Table<UserEntity>().Insert(user1);

            Repository.Commit(Guid.NewGuid());
        }
    }
}