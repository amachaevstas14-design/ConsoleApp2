using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task18_TryParseVsTryCatch : ITask
    {
        public string Title { get; } = "TryParse vs try/catch";

        public string Description { get; } =
            "Сравнить два способа обработки некорректного ввода числа.";

        /// <inheritdoc />
        public string[] Requirements { get; } = new[]
        {
            "Способ 1: int.TryParse возвращает bool и значение через out.",
        "Способ 2: int.Parse внутри try/catch с обработкой FormatException.",
        "Реализовать оба способа отдельными методами.",
        "Продемонстрировать работу на корректном и некорректном вводе."
        };

        /// <inheritdoc />
        public void Run()
        {
            string[] examples =
            {
                "123",
            "abc"
            };

            foreach (string text in examples)
            {
                Console.WriteLine($"Входная строка: \"{text}\"");

                Console.WriteLine("TryParse:");
                TryParse(text);

                Console.WriteLine("try/catch:");
                TryCatch(text);

                Console.WriteLine();
            }
        }


        private static void TryParse(string text)
        {
            if (!int.TryParse(text, out int result))
            {
                Console.WriteLine("Ошибка преобразования.");
                return;
            }

            Console.WriteLine($"Успешно: {result}");
        }

        private static void TryCatch(string text)
        {
            try
            {
                int result = int.Parse(text);
                Console.WriteLine($"Успешно: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка преобразования.");
            }
        }
    }
}
