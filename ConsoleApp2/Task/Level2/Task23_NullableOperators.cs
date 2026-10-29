using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task23_NullableOperators : ITask
    {
        private static readonly Dictionary<string, int> _settings =
            new Dictionary<string, int>()
            {
                ["volume"] = 12,
                ["fontSize"] = 14
            };

        public string Title { get; } = "Nullable-типы, ??, ?.";

        public string Description { get; } =
            "Продемонстрировать работу с nullable-типами: операторы ?? и ?., а также HasValue и Value.";

        public string[] Requirements { get; } = new[]
        {
            "Метод int? FindValue(string key) возвращает null, если ключа нет.",
            "Оператор ?? подставляет значение по умолчанию.",
            "Оператор ?. обеспечивает безопасный доступ к свойству объекта, который может быть null.",
            "Проверить свойства HasValue и Value с проверкой."
        };

        public void Run()
        {
            Console.WriteLine("=== int? и оператор ?? ===");

            int? found = FindValue("volume");

            Console.WriteLine($"FindValue(\"volume\") = {found}");
            Console.WriteLine($"HasValue: {found.HasValue}");
            Console.WriteLine($"Value с проверкой: {(found.HasValue ? found.Value : -1)}");
            Console.WriteLine($"Оператор ?? (found ?? 0): {found ?? 0}");

            Console.WriteLine();

            int? missing = FindValue("unknown");

            Console.WriteLine($"FindValue(\"unknown\") = {missing}");
            Console.WriteLine($"HasValue: {missing.HasValue}");
            Console.WriteLine($"Value с проверкой: {(missing.HasValue ? missing.Value : -1)}");
            Console.WriteLine($"Оператор ?? (missing ?? 100): {missing ?? 100}");

            Console.WriteLine();
            Console.WriteLine("=== Безопасный доступ через ?. ===");

            User user = FindUser("Иван");
            Console.WriteLine($"Город найденного пользователя: {user?.City}");

            User stranger = FindUser("Неизвестный");
            Console.WriteLine($"Город ненайденного пользователя: {stranger?.City}");
        }

        public static int? FindValue(string key)
        {
            if (_settings.TryGetValue(key, out int value))
            {
                return value;
            }

            return null;
        }

        public static User FindUser(string name)
        {
            return name == "Иван" ? new User(name, "Москва") : null;
        }
    }

    public sealed class User
    {
        public User(string name, string city)
        {
            Name = name;
            City = city;
        }

        public string Name { get; }

        public string City { get; }
    }
    
}
        
