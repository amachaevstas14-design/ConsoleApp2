using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task34_LinqWordGroups : ITask
    {
        private static readonly string[] Words = new[]
   {
       "конь",
        "кот",
        "кит",
        "конфета",
        "мак",
        "мама",
        "мяч",
        "морс",
        "солнце",
        "соловей",
        "яблоко",
        "юла"
        };

        public string Title { get; } = "LINQ: группировка слов по первой букве";

        public string Description { get; } =
            "Сгруппировать слова по первой букве, отсортировать группы и слова внутри групп.";

        public string[] Requirements { get; } = new[]
        {
            "Список из 10 и более слов.",
        "Группировать через LINQ GroupBy(w => w[0]).",
        "Отсортировать группы по первой букве.",
        "Внутри группы отсортировать слова.",
        "Вывести в формате «Буква: слово1, слово2, ...»."
        };

        public void Run()
        {
            var words = Words.ToList();

            Console.WriteLine($"Всего слов: {words.Count}");
            Console.WriteLine();

            var groups = words
                .GroupBy(w => w[0])
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    FirstLetter = g.Key,
                    Words = g.OrderBy(w => w, StringComparer.Ordinal).ToList()
                })
                .ToList();

            foreach (var group in groups)
            {
                string joined = string.Join(", ", group.Words);
                Console.WriteLine($"{char.ToUpperInvariant(group.FirstLetter)}: {joined}");
            }
        }
    }
}

