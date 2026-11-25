using FacadePattern_73464.Models;
using FacadePattern_73464.Interfaces;

namespace FacadePattern_73464.Services
{
    // Simulates authentication account management.
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Dictionary<int, string> _accounts = new();

        public void CreateAccount(User user)
        {
            _accounts.Add(user.Id, user.Email);

            Console.WriteLine(
                "[Authentication] Account created.");
        }

        public void UpdateAccount(User user)
        {
            _accounts[user.Id] = user.Email;

            Console.WriteLine(
                "[Authentication] Account updated.");
        }

        public void DeleteAccount(int userId)
        {
            _accounts.Remove(userId);

            Console.WriteLine(
                "[Authentication] Account removed.");
        }
    }
}