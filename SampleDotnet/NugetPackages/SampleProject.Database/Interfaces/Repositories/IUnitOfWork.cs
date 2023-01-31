using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Entities;
using System;

namespace SampleProject.Core.Interfaces.Repositories
{
    public interface IUnitOfWork<TDbContext>
        : ISharedRepository
        where TDbContext : DbContext
    {
        IEFRepository<TEntity> Table<TEntity>() where TEntity : BaseEntity;
    }

    public interface ISharedRepository : IDisposable
    {
        int SaveChanges();
    }
}