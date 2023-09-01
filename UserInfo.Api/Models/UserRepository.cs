using System.Data.SqlClient;
using UserInfo.Models;
using Dapper;

namespace UserInfo.Api.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users";
                return connection.Query<User>(sql);
            }
        }

        public User GetUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Id = @Id";
                return connection.QuerySingleOrDefault<User>(sql, new { Id = id });
            }
        }

        public User Add(User newUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Users (FirstName, LastName, Age, CardNumber, PhotoPath) VALUES (@FirstName, @LastName, @Age, @CardNumber, @PhotoPath); SELECT CAST(SCOPE_IDENTITY() as int)";

                newUser.Id = connection.Query<int>(sql, newUser).Single();
            }

            return newUser;
        }

        public User Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Users WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }

            return null;
        }

        public User Update(User updateUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Age = @Age, CardNumber = @CardNumber, PhotoPath = @PhotoPath WHERE Id = @Id";
                connection.Execute(sql, updateUser);
            }

            return updateUser;
        }
    }
}
