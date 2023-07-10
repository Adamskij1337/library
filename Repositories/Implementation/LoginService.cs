// LoginService.cs
using System.Data;
using System.Data.SqlClient;
using Library.Models.Domain;
using Library.Services;

namespace Library.Repositories.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly string _connectionString;

        public LoginService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool LoginUser(Login login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE UserName = @UserName AND Password = @Password", connection))
                {
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = login.UserName;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = login.Password;

                    var result = (int)command.ExecuteScalar();

                    return result > 0; // Jeśli wynik jest większy od 0, oznacza to, że dane logowania są poprawne
                }
            }
        }
    }
}
