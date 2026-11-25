using FacadePattern_73464.Interfaces;

namespace FacadePattern_73464.Services
{
    // Simulates sending notifications to users.
    public class NotificationService : INotificationService
    {
        public void SendNotification(string email, string message)
        {
            Console.WriteLine($"[Notification] To: {email}");
            Console.WriteLine($"[Notification] Message: {message}");
        }
    }
}