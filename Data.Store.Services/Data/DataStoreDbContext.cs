


using Data.Store.Services.Model;

namespace Data.Store.Services.Data;

public class DataStoreDbContext(DbContextOptions<DataStoreDbContext> options):DbContext(options)
{
    
    public DbSet<DataStore> DataStores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}