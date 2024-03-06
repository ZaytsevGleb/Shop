namespace Shop.Services.Catalog.DataAccess.Options;

public class DatabaseOptions
{
    public const string SectionName = "MongoDatabase";
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
}