using FacadePattern_73464.Models;

namespace FacadePattern_73464.Interfaces
{
    // Defines authentication account operations.
    public interface IAuthenticationService
    {
        void CreateAccount(User user);
        void UpdateAccount(User user);
        void DeleteAccount(int userId);
    }
}