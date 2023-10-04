using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TodoListStoreApi.Models;
using TodoListStoreApi.Services;

namespace TodoListStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TodoListService _todoListService;

        public TasksController(TodoListService todoListService) =>
            _todoListService = todoListService;
        
        [HttpGet]
        public async Task<List<TaskEntity>> Get() =>
            await _todoListService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TaskEntity>> Get(ObjectId id)
        {
            var task = await _todoListService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaskEntity newTask)
        {
            await _todoListService.CreateAsync(newTask);

            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(ObjectId id, TaskEntity updatedTask)
        {
            var task = await _todoListService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            updatedTask.Id = task.Id;

            await _todoListService.UpdateAsync(id, updatedTask);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var task = await _todoListService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            await _todoListService.RemoveAsync(id);

            return NoContent();
        }
    }
}