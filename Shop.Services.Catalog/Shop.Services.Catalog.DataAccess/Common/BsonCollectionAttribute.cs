namespace Shop.Services.Catalog.DataAccess.Common;

public class BsonCollectionAttribute(string collectionName) : Attribute
{
    public string CollectionName { get; } = collectionName;
}