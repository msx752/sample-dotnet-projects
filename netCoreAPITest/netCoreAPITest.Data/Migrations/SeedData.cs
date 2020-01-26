using Microsoft.EntityFrameworkCore;
using netCoreAPITest.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace netCoreAPITest.Data.Migrations
{
    public partial class ApiContext : DbContext
    {
        public async static Task SeedData(ApiContext apiContext)////makes Asnync
        {
            /*
             this is in-memory database so we have to call everytime this method
            
             */

            var p1 = new Personal()
            {
                Name = "Mustafa Salih",
                Surname = "ASLIM",
                Age = 29
            };
            if (await apiContext.Personals.AnyAsync(x => x == p1) || true)//for the real database with appropriate equality-conditions :D
            {
                apiContext.Personals.Add(p1);
            }

            var p2 = new Personal()
            {
                Name = "Üsame Fetullah",
                Surname = "AVCI",
                Age = 25
            };
            if (await apiContext.Personals.AnyAsync(x => x == p2) || true)//for the real database with appropriate equality-conditions :D
            {
                apiContext.Personals.Add(p2);
            }


            apiContext.SaveChanges();//updates database
        }

    }
}
