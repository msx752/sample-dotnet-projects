using Microsoft.EntityFrameworkCore;
using SampleDotnet.RepositoryFactory.Interfaces;

namespace Payment.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IUnitOfWork unitOfWork)
        {
            using (var context = unitOfWork.CreateRepository<PaymentDbContext>())
            {
                context.Database.EnsureCreated();
            }

            unitOfWork.SaveChanges();
        }
    }
}