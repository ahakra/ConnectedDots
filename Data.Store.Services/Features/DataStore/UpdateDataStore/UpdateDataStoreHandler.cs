using Data.Store.Services.CQRS;
using Data.Store.Services.Data;
using Data.Store.Services.Dtos;
using Data.Store.Services.Exceptions;
using FluentValidation;

namespace Data.Store.Services.Features.DataStore.UpdateDataStore;

public record UpdateDataStoreCommand(DataStoreDto dto) : ICommand<UpdateDataStoreResult>;

public record UpdateDataStoreResult(Guid Id);

public class UpdateDataStoreCommandValidator : AbstractValidator<UpdateDataStoreCommand>
{
    public UpdateDataStoreCommandValidator()
    {
        RuleFor(x => x.dto.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.dto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.dto.ConnectionString).NotEmpty().WithMessage("ConnectionString is required");
    }
}

public class UpdateDataStoreCommandHandler(DataStoreDbContext context)
    : ICommandHandler<UpdateDataStoreCommand, UpdateDataStoreResult>
{
    public async Task<UpdateDataStoreResult> Handle(UpdateDataStoreCommand command, CancellationToken cancellationToken)
    {
        var datastore = await context.DataStores.FindAsync(new object[] { command.dto.Id }, cancellationToken);

        if (datastore is null)
        {
            throw new NotFoundException("DataStore", command.dto.Id);
        }

        datastore.Name = command.dto.Name;
        datastore.ConnectionString = command.dto.ConnectionString;

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateDataStoreResult(datastore.Id);
    }
}