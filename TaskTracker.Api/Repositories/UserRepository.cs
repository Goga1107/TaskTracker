using Dapper;
using Microsoft.Data.Sqlite;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

        public async Task<int> CreateUser(User user)
        {
            using var con = GetConnection();
            var sql = @"insert into User(Username,Email)values(@Username,@Email);
                      select last_insert_rowid();";
                        
            var id = await con.ExecuteScalarAsync<long>(sql, user);
            return (int)id;

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using var con = GetConnection();
            var sql = "select * from user";
          return  await con.QueryAsync<User>(sql);
        }

        public async Task<bool> UpdateUser(User user)
        {
            using var db = GetConnection();
            var sql = "update user set Username = @Username, Email = @Email Where UserId = @UserId";
            var affected = await db.ExecuteAsync(sql, user);
            return affected > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            using var db = GetConnection();
            var sql = "delete from user where UserId =@id";
            var affected = await db.ExecuteAsync(sql, new {id});
            return affected > 0;
        }

        public async Task<User> GetUserById(int id)
        {
            using var db = GetConnection();
            var sql = "select * from user where UserId =@id";
            return await db.QueryFirstOrDefaultAsync<User>(sql, new {id});
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserId(int userid)
        {
          using var db = GetConnection();
            var sql = "select * from task where UserId=@userid";
            return await db.QueryAsync<TaskItem>(sql, new { userid });

        }
    }
}
