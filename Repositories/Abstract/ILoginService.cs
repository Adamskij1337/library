using Library.Models.Domain;

namespace Library.Services
{
    public interface ILoginService
    {
        bool LoginUser(Login login);
    }
}
