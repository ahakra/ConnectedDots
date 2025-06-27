using Data.Store.Services.Data;
using Data.Store.Services.Dtos;
using Data.Store.Services.Extensions;

namespace Data.Store.Services.Features.DataStore.GetDataStores;

public record GetDataStoresQuery() : IQuery<GetDataStoreResult>;

public record GetDataStoreResult(IEnumerable<DataStoreDto> result);
public class GetDataStoresQueryHandler(DataStoreDbContext context) :IQueryHandler<GetDataStoresQuery,GetDataStoreResult>
{
    public async Task<GetDataStoreResult> Handle(GetDataStoresQuery request, CancellationToken cancellationToken)
    {
        var dataStores = await context.DataStores.ToListAsync(cancellationToken);
        return new GetDataStoreResult(dataStores.ToDataStoreDto());
    }
}