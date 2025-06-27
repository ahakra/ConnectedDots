
using Azure.Core;
using Data.Store.Services.Dtos;
using Mapster;

namespace Data.Store.Services.Features.DataStore.CreateDataStore;

public record CreateDataStoreRequest(DataStoreDto dto) : ICommand<CreateDataStoreResult>;

public record CreateDataStoreResponse(Guid Id);

public class CreateDataStoreEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/data/store",
            async(CreateDataStoreRequest request,ISender sender)=>
            {
                var command = request.Adapt<CreateDataStoreCommand>();
                var result = await sender.Send(command);

                var respsonse = result.Adapt<CreateDataStoreResponse>();
                return Results.Created($"/data/store/{respsonse.Id}",respsonse);

            });
    }
}