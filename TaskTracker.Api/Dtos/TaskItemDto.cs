using TaskTracker.Models.Models;

namespace TaskTracker.Api.Dtos
{
    public class TaskItemDto
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TaskItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
