using System.Threading.Tasks;
using Library.Models.Domain;

namespace Library.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUser(User user);
        Task<User> GetUserByUsername(string username);
    }
}