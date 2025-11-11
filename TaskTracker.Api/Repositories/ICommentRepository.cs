using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public interface ICommentRepository
    {
        Task<int> CreateComment(Comment comment);
        Task<Comment> GetCommentById(int id);

        Task<bool> DeleteComment(int id);
        Task<IEnumerable<Comment>> GetRecentComments();
    }
}
