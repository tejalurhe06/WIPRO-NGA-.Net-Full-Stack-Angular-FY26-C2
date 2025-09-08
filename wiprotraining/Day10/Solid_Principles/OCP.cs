using System;

namespace OCP
{


    //Without OCP
    // public class NotificationService
    // {
    //     public void Send(string type)
    //     {
    //         if (type == "email")
    //         {
    //             Console.WriteLine("Sending Email");
    //         }
    //         else if (type == "sms")
    //         {
    //             Console.WriteLine("Sending SMS");
    //         }
    //     }
    // }

    //WITH OCP

    public interface INotification
    {
        void Send();
    }

    public class EmailNotification : INotification
    {
        public void Send()
        {
            Console.WriteLine("Sending Email");
        }

    }

    public class SmsNotification : INotification
    {
        public void Send()
        {
            Console.WriteLine("Sending SMS");
        }

    }

    public class NotificationService
    {
        public void SendNotification(INotification notification)
        {
            notification.send();
        }
    }
}