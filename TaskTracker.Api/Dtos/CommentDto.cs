namespace TaskTracker.Api.Dtos
{
    public class CommentDto
    {
        
        public int TaskId { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
