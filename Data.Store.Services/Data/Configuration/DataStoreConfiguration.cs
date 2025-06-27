using Data.Store.Services.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Store.Services.Data.Configuration;

public class DataStoreConfiguration:IEntityTypeConfiguration<DataStore>
{
    public void Configure(EntityTypeBuilder<DataStore> builder)
    {
        builder.ToTable("DataStores", schema: "integration"); 
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ConnectionString).IsRequired();
    }
}