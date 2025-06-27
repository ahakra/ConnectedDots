using Data.Store.Services.Dtos;
using Data.Store.Services.Model;

namespace Data.Store.Services.Extensions;

public static class DataStoreExtension
{
    public static IEnumerable<DataStoreDto> ToDataStoreDto(this IEnumerable<DataStore> dataStores)
    {
        return dataStores.Select(store => new DataStoreDto(
            Id: store.Id,
            Name: store.Name,
            ConnectionString: store.ConnectionString)).ToList();
    }
    
    public static DataStoreDto ToDataStoreDto(this DataStore dataStores)
    {
        return new DataStoreDto(
            Id: dataStores.Id,
            Name: dataStores.Name,
            ConnectionString: dataStores.ConnectionString);
    }
    
}