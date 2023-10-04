using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoListStoreApi.Models;

public class TaskEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("Name")]
    public string? TaskName { get; set; }

    [BsonElement("StartDate")]
    public string? StartDate { get; set; }

    [BsonElement("EndDate")]
    public string? EndDate { get; set; }
}