using Dapper;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;
        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);
        public async Task<int> CreateComment(Comment comment)
        {
            using var db = GetConnection();
            var sql = "insert into Comment(TaskId,Content,CreatedAt)values(@TaskId,@Content,@CreatedAt); " +
                "select last_insert_rowid();";
            var id = await db.ExecuteScalarAsync<long>(sql, comment);
            return (int)id;


        }

        public async Task<bool> DeleteComment(int id)
        {
            using var db = GetConnection();
            var sql = "delete from comment where CommentId =@id";
            var affected = await db.ExecuteAsync(sql, new { id });
            return affected > 0;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            using var db = GetConnection();
            var sql = "select * from comment where CommentId =@id";
            return await db.QueryFirstOrDefaultAsync<Comment>(sql, new { id });
        }

        public async Task<IEnumerable<Comment>> GetRecentComments()
        {
            using var connection = GetConnection();
            var sql = @"select * from Comment 
                where CreatedAt >= DATE('now', '-7 day')
                order by CreatedAt DESC;";
            return await connection.QueryAsync<Comment>(sql);
        }
    }
}
