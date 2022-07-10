using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;
using Samp.Core.Interfaces.Repositories.Shared;
using Samp.Database.Personal.Entities;

namespace Samp.Database.Personal
{
    public class MyContextSeed<TDbContext>
        : ContextSeed<TDbContext>
        where TDbContext : DbContext
    {
        public MyContextSeed(ISharedConnection<TDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
            var p1 = new PersonalEntity()
            {
                Id = 1,
                Name = "Ahmet",
                Surname = "FILAN",
                Age = 29,
                NationalId = "11111111111"
            };
            if (Connection.Db<PersonalEntity>().GetById(p1.Id) == null)
                Connection.Db<PersonalEntity>().Add(p1);

            var p2 = new PersonalEntity()
            {
                Id = 2,
                Name = "Fuat",
                Surname = "MUAT",
                Age = 21,
                NationalId = "333333333333"
            };
            if (Connection.Db<PersonalEntity>().GetById(p2.Id) == null)
                Connection.Db<PersonalEntity>().Add(p2);

            Connection.SaveChanges();
        }
    }
}