using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task07_StringReversal : ITask
    {
        public string Title { get; } = "Реверс строки без LINQ";

        public string Description { get; } = "Перевернуть введённую строку, обойдя её символы от конца к началу.";

        public string[] Requirements { get; } = new[]
        {
            "Не использовать Array.Reverse и LINQ.",
        "Реализовать через цикл for от конца к началу.",
        "Пустая строка и строка из одного символа обрабатываются корректно."
        };

        public void Run()
        {
            string input = ConsoleHelper.ReadLine("Введите строку: ");
            Console.WriteLine($"Результат: {Reverse(input)}");
        }

        private static string Reverse(string text)
        {
            char[] chars = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                chars[i] = text[text.Length - 1 - i];
            }

            return new string(chars);
        }
    }

}
