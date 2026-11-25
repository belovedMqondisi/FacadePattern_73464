using FacadePattern_73464.Models;

namespace FacadePattern_73464.Interfaces
{
    // Defines user storage operations.
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User? GetById(int id);
        List<User> GetAll();
        bool EmailExists(string email, int? excludeId = null);
    }
}