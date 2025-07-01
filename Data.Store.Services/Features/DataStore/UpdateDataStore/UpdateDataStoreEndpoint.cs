using Azure.Core;
using Data.Store.Services.Dtos;
using Mapster;

namespace Data.Store.Services.Features.DataStore.UpdateDataStore;

public record UpdateDataStoreRequest(DataStoreDto dto) : ICommand<UpdateDataStoreResult>;

public record UpdateDataStoreResponse(Guid Id);

public class UpdateDataStoreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/data/store",
            async (UpdateDataStoreRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateDataStoreCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateDataStoreResponse>();
                return Results.Ok(response);
            });
    }
}