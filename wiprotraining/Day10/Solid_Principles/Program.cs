using System;
using OCP;
class Program
{
    static void Main(string[] args)
    {
        INotification notification = new EmailNotification();

        NotificationService service = new NotificationService();

        service.SendNotification(notification);

    }
}