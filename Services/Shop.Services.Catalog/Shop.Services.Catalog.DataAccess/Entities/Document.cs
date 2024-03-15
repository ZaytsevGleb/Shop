using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Services.Catalog.DataAccess.Entities;

public class Document : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    [BsonElement("createdAt")]
    public DateTime? CreatedAt { get; set; }
    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}