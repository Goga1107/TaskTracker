using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> GetUserById(int id);
        Task<IEnumerable<TaskItem>> GetTasksByUserId(int userid);
    }
}
