using Dapper;
using Microsoft.Data.Sqlite;
using TaskTracker.Api.Dtos;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);
   
        public async Task<int> CreateTask(TaskItem task)
        {
            using var con = GetConnection();
            var sql = @"insert into task(UserId,Title,Description,Status,CreatedAt)values(@UserId,@Title,@Description,@Status,@CreatedAt);
                        select last_insert_rowid();";
            var id = await con.ExecuteScalarAsync<long>(sql, task);
            return (int)id;


        }

        public async Task<bool> DeleteTask(int id)
        {
          
            using var dbb = GetConnection();
            
            var sql = @"Delete from Task where TaskId = @id";
            var affected = await dbb.ExecuteAsync(sql, new { id });
            return affected > 0;

        }

        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            using var db = GetConnection();
          
            var sql = @"Select * from task";
            return await db.QueryAsync<TaskItem>(sql);
        }

        public async Task<TaskItem> GetTaskById(int id)
        {
            using var db = GetConnection();
            var sql = @"select * from task where TaskId = @id";
            return await db.QueryFirstOrDefaultAsync<TaskItem>(sql, new {id});
        }

        public async Task<bool> UpdateTask(TaskItem task)
        {
            using var con = GetConnection();
            var sql = @"update task set UserId = @UserId,Title=@Title,Description = @Description,Status=@Status where TaskId = @TaskId";
            var affected = await con.ExecuteAsync(sql,task);
            return affected > 0;
        }

        public async Task<IEnumerable<TaskStatusCountDto>> GetTaskCountByStatus()
        {
            using var connection = GetConnection();
            var sql = "select Status, count(*) as count from task group by Status;";
            return await connection.QueryAsync<TaskStatusCountDto>(sql);
        }
    }
}
