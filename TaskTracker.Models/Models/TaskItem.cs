namespace TaskTracker.Models.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
