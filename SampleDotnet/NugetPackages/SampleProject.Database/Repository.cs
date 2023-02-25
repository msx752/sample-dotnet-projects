namespace SampleProject.Database;

public class Repository<TDbContext>
    : IRepository<TDbContext>, IDisposable
    where TDbContext : DbContext
{
    private readonly TDbContext _context;
    private bool disposedValue;

    public DatabaseFacade Database { get => _context.Database; }

    public Repository(TDbContext dbContext)
    {
        _context = dbContext;
        RepositoryEntryEventHandler(_context);
    }

    public IQueryable<T> AsQueryable<T>() where T : class
    {
        return _context.Set<T>().AsQueryable<T>();
    }

    public void Delete<T>(T entity) where T : class
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Deleted;
    }

    public void Delete<T>(params T[] entities) where T : class
    {
        foreach (var item in entities)
            Delete(item);
    }

    public void Delete<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var item in entities)
            Delete(item);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public T? Find<T>(params object[] keyValues) where T : class
    {
        return _context.Set<T>().Find(keyValues);
    }

    public T? FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        IQueryable<T> query = AsQueryable<T>();
        return query.FirstOrDefault(predicate);
    }

    public T? GetById<T>(object id) where T : class
    {
        return Find<T>(id);
    }

    public void Insert<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
    }

    public void Insert<T>(params T[] entities) where T : class
    {
        _context.Set<T>().AddRange(entities);
    }

    public void Insert<T>(IEnumerable<T> entities) where T : class
    {
        _context.Set<T>().AddRange(entities);
    }

    public int SaveChanges()
    {
        var result = _context.SaveChanges();
        return result;
    }

    public void Update<T>(T entity) where T : class
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
    }

    public void Update<T>(params T[] entities) where T : class
    {
        for (int i = 0; i < entities.Length; i++)
            Update<T>(entities[i]);
    }

    public void Update<T>(IEnumerable<T> entities) where T : class
    {
        for (int i = 0; i < entities.Count(); i++)
            Update<T>(entities.ElementAt(i));
    }

    public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return AsQueryable<T>().Where(predicate);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                RepositoryEntryEventHandler(_context, true);
                _context.Dispose();
            }

            disposedValue = true;
        }
    }

    private static void RepositoryEntryEventHandler(DbContext _context, bool disposing = false)
    {
        try
        {
            var serviceProvider = _context.GetInfrastructure();
            var entryEventNotifier = (IRepositoryEntryNotifier?)serviceProvider?.GetService(typeof(IRepositoryEntryNotifier));
            if (serviceProvider != null && entryEventNotifier != null)
            {
                _context.ChangeTracker.Tracked -= (sender, e) => entryEventNotifier.RepositoryEntryEvent(sender, e, _context, serviceProvider);
                if (!disposing)
                    _context.ChangeTracker.Tracked += (sender, e) => entryEventNotifier.RepositoryEntryEvent(sender, e, _context, serviceProvider);

                _context.ChangeTracker.StateChanged -= (sender, e) => entryEventNotifier.RepositoryEntryEvent(sender, e, _context, serviceProvider);
                if (!disposing)
                    _context.ChangeTracker.StateChanged += (sender, e) => entryEventNotifier.RepositoryEntryEvent(sender, e, _context, serviceProvider);
            }
        }
        catch (Exception e)
        {
        }
    }
}