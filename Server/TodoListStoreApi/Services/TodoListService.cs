    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
    using TodoListStoreApi.Models;

    namespace TodoListStoreApi.Services
    {
        public class TodoListService
        {
            private readonly IMongoCollection<TaskEntity> _todoListCollecion;

            public TodoListService(IOptions<TodoListStoreDatabaseSettings> todoListStoreDatabaseSettings)
            {
                var mongoClient = new MongoClient(todoListStoreDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(todoListStoreDatabaseSettings.Value.DatabaseName);

                _todoListCollecion = mongoDatabase.GetCollection<TaskEntity>(todoListStoreDatabaseSettings.Value.TasksCollecionName);
            }

            public async Task<List<TaskEntity>> GetAsync() =>
                await _todoListCollecion.Find(_ => true).ToListAsync();
            
            public async Task<TaskEntity?> GetAsync(ObjectId id) =>
                await _todoListCollecion.Find(x => x.Id == id).FirstOrDefaultAsync();
            
            public async Task CreateAsync(TaskEntity newTask) =>
                await _todoListCollecion.InsertOneAsync(newTask);
            
            public async Task UpdateAsync(ObjectId id, TaskEntity updatedTask) =>
                await _todoListCollecion.ReplaceOneAsync(x => x.Id == id, updatedTask);
            
            public async Task RemoveAsync(ObjectId id) =>
                await _todoListCollecion.DeleteOneAsync(x => x.Id == id);
        }
    }