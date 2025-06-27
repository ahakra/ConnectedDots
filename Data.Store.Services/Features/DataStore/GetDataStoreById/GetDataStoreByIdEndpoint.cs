using Data.Store.Services.Dtos;
using Mapster;

namespace Data.Store.Services.Features.DataStore.GetDataStoreById;


public record GetDataStoreByIdRequest(Guid Id);

public record GetDataStoreByIdResponse(DataStoreDto res);


public class GetDataStoreByIdEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/data/store/{id}", async (Guid id,ISender sender) =>
        {
            var req = new GetDataStoreByIdRequest(id);
            var query = req.Adapt<GetDataStoreByIdQuery>();

            var result = await sender.Send(query);
            var response = result.Adapt<GetDataStoreByIdResponse>();
            return Results.Ok(response);

        });
    }
}