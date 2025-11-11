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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapp;
        public CommentController(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapp = mapper;
        }
        /// <summary>
        /// Gets one comment by id.
        /// </summary>
        /// <param name="id">comment ID</param>
        /// <returns>One comment</returns>
        /// <response code="200">Returns the requested comment</response>
        /// <response code="404">If the comment is not found</response>
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var com = await _repository.GetCommentById(id);
            if (com == null)
                return NotFound();

            var dto = _mapp.Map<CommentDto>(com);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="user">comment content</param>
        /// <returns>created comment with assigned Id</returns>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Validation error</response>
        [HttpPost]

        public async Task<IActionResult> CreateComment(CommentDto com)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapp.Map<Comment>(com);
            var id = await _repository.CreateComment(entity);
            entity.CreatedAt = DateTime.Now;
            

            return CreatedAtAction(nameof(GetById), new { id }, com);
        }

        /// <summary>
        /// Gets recent comments 7 day.
        /// </summary>
        /// <returns>recent comments</returns>
        /// <response code="200">Returns  7 day  comments</response>
        /// <response code="404">If the comment is not found</response>

        [HttpGet]
        public async Task<IActionResult> GetRecentComments()
        {
            var comments = await _repository.GetRecentComments();
            if (!comments.Any())
                return NotFound("No recent comments found.");

            return Ok(comments);
        }


            /// <summary>
            /// Deletes a comment by id.
            /// </summary>
            /// <param name="id">comment ID</param>
            /// <response code="204">Successfully deleted</response>
            /// <response code="404">If the comment is not found</response>
            [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteComment(int id)
        {
            var success = await _repository.DeleteComment(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
