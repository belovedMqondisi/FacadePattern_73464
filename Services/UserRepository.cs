using FacadePattern_73464.Models;
using FacadePattern_73464.Interfaces;

namespace FacadePattern_73464.Services
{
    // Manages user storage and CRUD operations.
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public void Add(User user)
        {
            user.Id = _nextId++;

            _users.Add(new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });

            Console.WriteLine("[Repository] User saved.");
        }

        public void Update(User user)
        {
            var existing = _users.Find(u => u.Id == user.Id);

            if (existing == null)
                throw new InvalidOperationException("User not found.");

            existing.Name = user.Name;
            existing.Email = user.Email;

            Console.WriteLine("[Repository] User updated.");
        }

        public void Delete(int id)
        {
            var user = _users.Find(u => u.Id == id);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            _users.Remove(user);

            Console.WriteLine("[Repository] User deleted.");
        }

        public User? GetById(int id)
        {
            var user = _users.Find(u => u.Id == id);

            return user == null ? null : new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public List<User> GetAll()
        {
            return _users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }

        public bool EmailExists(string email, int? excludeId = null)
        {
            return _users.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                && u.Id != excludeId);
        }
    }
}