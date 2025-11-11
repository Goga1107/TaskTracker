using TaskTracker.Api.Dtos;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasks();
        Task<TaskItem> GetTaskById(int id);
        Task<int> CreateTask(TaskItem task);
        Task<bool> UpdateTask(TaskItem task);

        Task<bool> DeleteTask(int id);
        Task<IEnumerable<TaskStatusCountDto>> GetTaskCountByStatus();
    }
}
