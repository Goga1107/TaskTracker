namespace TaskTracker.Models.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
