using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models.Domain;

namespace Library.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly DatabaseContext _context;

        public RegistrationService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                // Sprawdź, czy użytkownik o podanej nazwie już istnieje w bazie danych
                if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
                    return false;

                // Wykonaj polecenie SQL INSERT INTO
                string sql = "INSERT INTO Users (UserName, Password, Email) VALUES ({0}, {1}, {2})";
                await _context.Database.ExecuteSqlRawAsync(sql, user.UserName, user.Password, user.Email);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
