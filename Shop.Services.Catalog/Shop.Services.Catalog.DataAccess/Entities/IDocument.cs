using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Services.Catalog.DataAccess.Entities;

public interface IDocument
{
    public string Id { get; set; }
}