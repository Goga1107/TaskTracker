using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Mapper;
using TaskTracker.Api.Repositories;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapp;

        public UserController(IUserRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapp = mapper;
        }
        /// <summary>
        /// Gets one user by id.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>One user</returns>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">If the user is not found</response>
        [HttpGet("{id}")]
   
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetUserById(id);
            if (user == null)
                return NotFound();

            var dto = _mapp.Map<UserDto>(user);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>created user with assinged Id</returns>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Validation error</response>
        [HttpPost]
     
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapp.Map<User>(user);
            var id = await _repository.CreateUser(entity);

            user.UserId = id;

            return CreatedAtAction(nameof(GetById), new { id }, user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List of all users</returns>
        /// <response code="200">Returns list of users</response>
        /// <response code="404">If no users are found</response>
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var all = await _repository.GetUsers();
            if (all == null)
                return NotFound();

            var dto = _mapp.Map<IEnumerable<UserDto>>(all);
            return Ok(dto);
        }

        /// <summary>
        /// Updates an existing user by id.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="dto">Updated user data</param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If the user is not found</response>
        [HttpPut("{id}")]
  
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            var user = _mapp.Map<User>(dto);
            user.UserId = id;

            var success = await _repository.UpdateUser(user);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a user by id.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If the user is not found</response>
        [HttpDelete("{id}")]
    
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _repository.DeleteUser(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        /// <summary>
        /// Gets tasks  by user id.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <response code="200">Successfully Returned</response>
        /// <response code="404">If the task with that user id not found</response>
        [HttpGet("user/{userid}")]
        public async Task<IActionResult> GetTasksByUser(int userid)
        {
            var tasks = await _repository.GetTasksByUserId(userid);
            if (!tasks.Any())
                return NotFound($"No tasks found for user {userid}");

            var dto = _mapp.Map<IEnumerable<TaskItemDto>>(tasks);
            return Ok(dto);
        }
    }
}
