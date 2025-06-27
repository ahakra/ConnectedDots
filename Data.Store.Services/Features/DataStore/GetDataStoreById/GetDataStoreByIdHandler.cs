using Data.Store.Services.Data;
using Data.Store.Services.Dtos;
using Data.Store.Services.Exceptions;
using Data.Store.Services.Extensions;

namespace Data.Store.Services.Features.DataStore.GetDataStoreById;


public record GetDataStoreByIdQuery(Guid Id) : IQuery<GetDataStoreByIdResult>;

public record GetDataStoreByIdResult(DataStoreDto result);

public class GetDataStoreByIdQueryHandler(DataStoreDbContext context) 
    : IQueryHandler<GetDataStoreByIdQuery,GetDataStoreByIdResult>
{
    public async Task<GetDataStoreByIdResult> Handle(GetDataStoreByIdQuery query, CancellationToken cancellationToken)
    {
        var dataStore = await context.DataStores
            .Where(o => o.Id == query.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (dataStore == null)
            throw new NotFoundException("DataStore", query.Id);

        return new GetDataStoreByIdResult(dataStore.ToDataStoreDto());
    }
}