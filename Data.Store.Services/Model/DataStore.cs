namespace Data.Store.Services.Model;

public class DataStore
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;

}