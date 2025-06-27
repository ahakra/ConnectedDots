using Data.Store.Services.Dtos;
using Mapster;

namespace Data.Store.Services.Features.DataStore.GetDataStores;

public record GetDataStoresRequest();

public record GetDataStoresReponse(IEnumerable<DataStoreDto> res);

public class GetDataStoresEndpoint :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/data/store", async (ISender sender) =>
        {
            var req = new GetDataStoresRequest();
            var query = req.Adapt<GetDataStoresQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetDataStoresReponse>();
            return Results.Ok(response);
        });
    }
}