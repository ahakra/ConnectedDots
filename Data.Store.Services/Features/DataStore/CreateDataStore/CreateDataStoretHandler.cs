using Data.Store.Services.CQRS;
using Data.Store.Services.Data;
using Data.Store.Services.Dtos;
using FluentValidation;

namespace Data.Store.Services.Features.DataStore.CreateDataStore;


public record CreateDataStoreCommand(DataStoreDto dto) : ICommand<CreateDataStoreResult>;

public record CreateDataStoreResult(Guid Id);

public class CreateDataStoreCommandValidator : AbstractValidator<CreateDataStoreCommand>
{
    public CreateDataStoreCommandValidator()
    {
        RuleFor(x => x.dto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.dto.ConnectionString).NotEmpty().WithMessage("ConnectionString is required");
    }
}
public class CreateDataStoreCommandHandler(DataStoreDbContext context):ICommandHandler<CreateDataStoreCommand,CreateDataStoreResult>
{
    public async Task<CreateDataStoreResult> Handle(CreateDataStoreCommand command, CancellationToken cancellationToken)
    {
        var datastore = new Model.DataStore
        {
            Id = Guid.NewGuid(),
            Name = command.dto.Name,
            ConnectionString = command.dto.ConnectionString,
        };
        context.DataStores.Add(datastore);
       await context.SaveChangesAsync(cancellationToken);
       return new CreateDataStoreResult(datastore.Id);

    }
}