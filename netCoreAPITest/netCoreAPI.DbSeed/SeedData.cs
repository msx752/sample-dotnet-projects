using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Model.Entities;
using System;

namespace netCoreAPI.Static.DbSeed
{
    public static class MyContextSeed
    {
        private static bool initiated = false;

        public static void SeedData(ISharedConnection unitOfWork)
        {
            if (initiated)
                return;
            initiated = true;
            /*
                we have in-memory database so we have to call always
             */
            var p1 = new Personal()
            {
                Id = 1,
                Name = "Ahmet",
                Surname = "FILAN",
                Age = 29,
                NationalId = "11111111111"
            };
            if (unitOfWork.Db<Personal>().GetById(p1.Id) == null)
                unitOfWork.Db<Personal>().Add(p1);

            var p2 = new Personal()
            {
                Id = 2,
                Name = "Mehmet",
                Surname = "SAVCI",
                Age = 21,
                NationalId = "333333333333"
            };
            if (unitOfWork.Db<Personal>().GetById(p2.Id) == null)
                unitOfWork.Db<Personal>().Add(p2);

            //triggers saveChanges for the updating Database
            unitOfWork.SaveChanges();

            //database has been updated
            if (unitOfWork.Db<Personal>().GetById(p2.Id) != null)
                Console.WriteLine("this user is already in Database");
        }
    }
}