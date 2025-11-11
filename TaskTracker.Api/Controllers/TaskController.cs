using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Repositories;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repo;
        private readonly IMapper _mapper;
      
        public TaskController(ITaskRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
         
        }

        /// <summary>
        /// Gets one task by id.
        /// </summary>
        /// <returns>one task.</returns>
        /// <response code="200">Returns task</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var task = await _repo.GetTaskById(id);
            if (task == null) 
            return NotFound();

            var dto = _mapper.Map<TaskItemDto>(task);
            return Ok(dto);

        }
        /// <summary>
        /// creates task.
        /// </summary>
        /// <response code="201">successfully created </response>
        /// <response code="400">validation error</response>

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = _mapper.Map<TaskItem>(dto);
            task.CreatedAt = DateTime.Now;
            var id = await _repo.CreateTask(task);
            dto.TaskId = id;
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }


        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>List of task items.</returns>
        /// <response code="200">Returns the list of tasks</response>
        /// <response code="404">if the tasks not found</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _repo.GetAllTasks();
            if(tasks == null)
                return NotFound();

           var dto =_mapper.Map<IEnumerable<TaskItemDto>>(tasks);
            return Ok(dto);
            
        }
        /// <summary>
        /// updates tasks by its id.
        /// </summary>
        /// <response code="204">success but nothing to return.</response>
        /// <response code="404">if the task not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasks(int id,TaskItemDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);

            task.TaskId = id;

         var success = await _repo.UpdateTask(task);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// deletes tasks by its id.
        /// </summary>
        /// <response code="204">success but nothing to return.</response>
        /// <response code="404">if the task not found</response>
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteTask(int id)
        {
       

            var success = await _repo.DeleteTask(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        /// <summary>
        /// gets tasks count by status.
        /// </summary>
        /// <response code="200">successfully returned count</response>
        /// <response code="404">if the task not found</response>
        [HttpGet("count-by-status")]
        public async Task<IActionResult> GetTaskCountByStatus()
        {
            var result = await _repo.GetTaskCountByStatus();
            return Ok(result);
        }

        [HttpGet("test-exception")]
        public IActionResult ThrowError()
        {
            throw new Exception("Test exception triggered");
        }
    }
}
