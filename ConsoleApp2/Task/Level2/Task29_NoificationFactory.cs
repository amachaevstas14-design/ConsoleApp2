using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task29_NoificationFactory: ITask
    {
        public string Title { get; } = "Паттерн «Фабрика» (уведомления)";

        public string Description { get; } =
            "Реализовать фабрику уведомлений: выбор реализации по значению enum.";

        public string[] Requirements { get; } = new[]
        {
            "Интерфейс INotification с методом Send(string message).",
        "Реализации: EmailNotification, SmsNotification и PushNotification.",
        "NotificationFactory.Create(NotificationType type) возвращает нужную реализацию.",
        "Продемонстрировать создание и отправку уведомлений через фабрику."
        };

        public void Run()
        {
            string message = "Ваш заказ отправлен!";

            foreach (NotificationType type in Enum.GetValues(typeof(NotificationType)))
            {
                INotification notification = NotificationFactory.Create(type);

                Console.WriteLine($"Создан канал: {notification.GetType().Name}");
                notification.Send(message);
                Console.WriteLine();
            }
        }
    }

    public enum NotificationType
    {
        Email,
        Sms,
        Push
    }

    public interface INotification
    {
        void Send(string message);
    }

    public sealed class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Email] Отправлено письмо: {message}");
        }
    }

    public sealed class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Sms] Отправлено SMS: {message}");
        }
    }

    public sealed class PushNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Push] Отправлено push-уведомление: {message}");
        }
    }

    public static class NotificationFactory
    {
        public static INotification Create(NotificationType type)
        {
            if (type == NotificationType.Email)
            {
                return new EmailNotification();
            }

            if (type == NotificationType.Sms)
            {
                return new SmsNotification();
            }

            if (type == NotificationType.Push)
            {
                return new PushNotification();
            }

            throw new ArgumentOutOfRangeException(
                nameof(type), type, "Неизвестный тип уведомления.");
        }
    }

}

