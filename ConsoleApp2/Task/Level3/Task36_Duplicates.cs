using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task36_Duplicates : ITask
    {
        public string Title { get; } = "Поиск дубликатов (HashSet + LINQ)";

        public string Description { get; } =
            "Даны два способа найти повторяющиеся значения в списке чисел: " +
            "через HashSet (Contains) и через LINQ (GroupBy + Where). " +
            "Оба подхода отсортированы, и их результаты сравниваются.";

        public string[] Requirements { get; } = new[]
        {
        "Метод FindDuplicatesWithHashSet возвращает отсортированный список дубликатов.",
        "Метод FindDuplicatesWithLinq группирует элементы через GroupBy и оставляет группы, где Count > 1.",
        "Для обоих методов проверяется, что результаты совпадают.",
    };

        public static List<int> FindDuplicatesWithHashSet(IEnumerable<int> numbers)
        {
            var seen = new HashSet<int>();
            var duplicates = new HashSet<int>();

            foreach (var n in numbers)
            {
                if (!seen.Add(n))
                {
                    duplicates.Add(n);
                }
            }

            return duplicates.OrderBy(x => x).ToList();
        }

        public static List<int> FindDuplicatesWithLinq(IEnumerable<int> numbers)
        {
            return numbers
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .OrderBy(x => x)
                .ToList();
        }

        public void Run()
        {
            var numbers = new List<int>
        {
            1, 2, 3, 2, 5, 6, 5, 7, 3, 9, 1, 9, 11, 11, 11, 13, 4, 4, 2
        };

            Console.WriteLine(Title);
            Console.WriteLine($"Всего элементов: {numbers.Count}");

            var viaHashSet = FindDuplicatesWithHashSet(numbers);
            Console.WriteLine($"Дубликаты (HashSet): {string.Join(", ", viaHashSet)}");

            var viaLinq = FindDuplicatesWithLinq(numbers);
            Console.WriteLine($"Дубликаты (LINQ):    {string.Join(", ", viaLinq)}");

            var equal = viaHashSet.SequenceEqual(viaLinq);
            Console.WriteLine($"Результаты совпадают: {(equal ? "да" : "нет")}");
            Console.WriteLine(
                "Пояснение: HashSet отслеживает, был ли элемент уже встречен (Add возвращает false " +
                "для повторного элемента). LINQ группирует значения через GroupBy и оставляет группы, " +
                "в которых больше одного элемента (Where(g => g.Count() > 1)).");

            Console.WriteLine();
            Console.WriteLine("Пустая последовательность:");
            var empty = new List<int>();
            var emptyHash = FindDuplicatesWithHashSet(empty);
            var emptyLinq = FindDuplicatesWithLinq(empty);
            Console.WriteLine($"HashSet: {emptyHash.Count} дубликатов, LINQ: {emptyLinq.Count} дубликатов");

            Console.WriteLine();
            Console.WriteLine("Список без дубликатов:");
            var unique = new List<int> { 5, 2, 8, 1 };
            var uniqueHash = FindDuplicatesWithHashSet(unique);
            var uniqueLinq = FindDuplicatesWithLinq(unique);
            Console.WriteLine($"HashSet: {uniqueHash.Count} дубликатов, LINQ: {uniqueLinq.Count} дубликатов");
        }
    }
}
