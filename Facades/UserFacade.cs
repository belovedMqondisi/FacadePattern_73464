using System.ComponentModel.DataAnnotations;
using FacadePattern_73464.Models;
using FacadePattern_73464.Interfaces;
using FacadePattern_73464.Services;

namespace FacadePattern_73464.Facades
{
    // Provides a unified interface for user management.
    public class UserFacade
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationService _authentication;
        private readonly INotificationService _notification;

        public UserFacade()
            : this(new UserRepository(),
                   new AuthenticationService(),
                   new NotificationService())
        {
        }

        // Allows internal services to be replaced.
        public UserFacade(
            IUserRepository repository,
            IAuthenticationService authentication,
            INotificationService notification)
        {
            _repository = repository;
            _authentication = authentication;
            _notification = notification;
        }

        // Adds a user and creates an authentication account.
        public void AddUser(User user)
        {
            ValidateUser(user);

            if (_repository.EmailExists(user.Email))
                throw new InvalidOperationException(
                    "Email already exists.");

            _repository.Add(user);
            _authentication.CreateAccount(user);

            _notification.SendNotification(
                user.Email,
                $"Welcome, {user.Name}! Your account has been created.");

            Console.WriteLine("User added successfully.");
        }

        // Updates user information and authentication details.
        public void UpdateUser(User user)
        {
            ValidateUser(user);

            if (_repository.GetById(user.Id) == null)
                throw new InvalidOperationException(
                    "User not found.");

            if (_repository.EmailExists(user.Email, user.Id))
                throw new InvalidOperationException(
                    "Email already exists.");

            _repository.Update(user);
            _authentication.UpdateAccount(user);

            _notification.SendNotification(
                user.Email,
                "Your account information has been updated.");

            Console.WriteLine("User updated successfully.");
        }

        // Removes a user and the associated account.
        public void DeleteUser(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
                throw new InvalidOperationException(
                    "User not found.");

            _repository.Delete(id);
            _authentication.DeleteAccount(id);

            _notification.SendNotification(
                user.Email,
                "Your account has been deleted.");

            Console.WriteLine("User deleted successfully.");
        }

        // Retrieves a single user.
        public User? GetUser(int id)
        {
            return _repository.GetById(id);
        }

        // Retrieves all registered users.
        public List<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

        // Validates user information before processing.
        private static void ValidateUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ValidationException(
                    "Name is required.");

            if (string.IsNullOrWhiteSpace(user.Email) ||
                !new EmailAddressAttribute().IsValid(user.Email))
                throw new ValidationException(
                    "Enter a valid email address.");
        }
    }
}