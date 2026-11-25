namespace FacadePattern_73464.Interfaces
{
    // Defines notification operations.
    public interface INotificationService
    {
        void SendNotification(string email, string message);
    }
}