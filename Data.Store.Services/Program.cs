using Carter;
using Data.Store.Services.Behaviors;
using Data.Store.Services.Data;
using Data.Store.Services.Exceptions.Handlers;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//services part
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
    conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddDbContext<DataStoreDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DataStoreDatabase");
    options.UseSqlServer(connectionString);
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });


app.Run();