using Microsoft.Data.Sqlite;

namespace TaskTracker.Api.Infrastructure
    
{
    public class DbInitializer
    {

        public static void Initialize(string connectionString)
        {
            using var connect = new SqliteConnection(connectionString);
            connect.Open();

            var cmd = connect.CreateCommand();
            cmd.CommandText = @"Create table if not exists User(
                                 UserId integer primary key autoincrement,
                                 Username text not null,
                                 Email text not null);

                                                        
                               Create table if not exists Task(
                               TaskId integer primary key autoincrement,
                               UserId integer not null,
                               Title text not null,
                               Description text not null,
                               Status integer not null,
                              CreatedAt text not null,
                               foreign key(UserId) references User(UserId) ON DELETE CASCADE);

               
             Create table if not exists Comment
                  (
                     CommentId integer primary key autoincrement,
                     TaskId integer not null,
                      Content text not null,
                      CreatedAt text not null,
                      foreign key(TaskId) references Task(TaskId) ON DELETE CASCADE
                        );";
             cmd.ExecuteNonQuery();


                                
        }
    }
}
