using Microsoft.EntityFrameworkCore;
using SampleDotnet.RepositoryFactory.Interfaces;

namespace Cart.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IUnitOfWork unitOfWork)
        {
            using (var context = unitOfWork.CreateRepository<CartDbContext>())
            {
                context.Database.EnsureCreated();
            }

            unitOfWork.SaveChanges();
        }
    }
}