using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task38_ListVsLinkedList : ITask
    {
        private const int ElementCount = 100_000;
        private const int InsertCount = 1000;
        private const int AccessCount = 10_000;

        public string Title { get; } = "List vs LinkedList: вставка и доступ по индексу";

        public string Description { get; } =
            "В два списка, заполненные 100 000 чисел, вставляется 1000 значений в середину. " +
            "Для List каждый раз сдвигаются элементы (O(n)), а в LinkedList узел вставляется " +
            "за константное время. Отдельно измеряется доступ по индексу: у List он O(1), " +
            "а у LinkedList требуется пройтись по цепочке узлов (O(n)).";

        public string[] Requirements { get; } = new[]
        {
            "Обе коллекции заполняются 100 000 элементов.",
        "1000 вставок в середину: List.Insert в сравнении с AddAfter у LinkedList.",
        "Замер времени вставок через Stopwatch.",
        "Замер доступа к среднему элементу (10 000 обращений) для обоих типов.",
        "Вывод: где LinkedList быстрее, а где List.",
        };

        public static void Fill(LinkedList<int> list, int count)
        {
            for (var i = 0; i < count; i++)
            {
                list.AddLast(i);
            }
        }

        public static int GetAt(LinkedList<int> list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона цепочки");
            }

            var node = list.First;
            for (var i = 0; i < index && node != null; i++)
            {
                node = node.Next;
            }
            if (node is null)
            {
                throw new InvalidOperationException("Цепочка короче, чем запрошенный индекс");
            }

            return node.Value;
        }

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine($"Элементов в каждой коллекции: {ElementCount:N0}");
            Console.WriteLine($"Вставок в середину: {InsertCount:N0}");

            var list = new List<int>(Enumerable.Range(0, ElementCount));
            var linkedList = new LinkedList<int>();
            Fill(linkedList, ElementCount);

            var insertStopwatch = Stopwatch.StartNew();
            for (var i = 0; i < InsertCount; i++)
            {
                var mid = list.Count / 2;
                list.Insert(mid, i);
            }
            insertStopwatch.Stop();
            var listInsertTime = insertStopwatch.Elapsed;

            var linkedInsertStopwatch = Stopwatch.StartNew();
            var middle = linkedList.First;
            for (var i = 0; i < ElementCount / 2 && middle != null; i++)
            {
                middle = middle.Next;
            }
            var anchor = middle;
            for (var i = 0; i < InsertCount; i++)
            {
                anchor = linkedList.AddAfter(anchor, i);
            }
            linkedInsertStopwatch.Stop();
            var linkedInsertTime = linkedInsertStopwatch.Elapsed;

            Console.WriteLine($"Вставка в середину: List — {listInsertTime.TotalSeconds:0.000} s, LinkedList — {linkedInsertTime.TotalSeconds:0.000} s");
            Console.WriteLine($"Размер List: {list.Count:N0}, LinkedList: {linkedList.Count:N0}");

            var probeIndex = ElementCount / 2 - 1;

            var listAccessStopwatch = Stopwatch.StartNew();
            var sumList = 0;
            for (var i = 0; i < AccessCount; i++)
            {
                sumList += list[probeIndex];
            }
            listAccessStopwatch.Stop();
            var listAccessTime = listAccessStopwatch.Elapsed;

            var linkedAccessStopwatch = Stopwatch.StartNew();
            var sumLinked = 0;
            for (var i = 0; i < AccessCount; i++)
            {
                sumLinked += GetAt(linkedList, probeIndex);
            }
            linkedAccessStopwatch.Stop();
            var linkedAccessTime = linkedAccessStopwatch.Elapsed;

            Console.WriteLine($"Доступ к элементу №{probeIndex:N0} ({AccessCount:N0} раз): List — {listAccessTime.TotalSeconds:0.000} s, LinkedList — {linkedAccessTime.TotalSeconds:0.000} s");
            Console.WriteLine($"Значения совпали: {(sumList == sumLinked ? "да" : "нет")}");

            Console.WriteLine();
            Console.WriteLine("Выводы:");
            Console.WriteLine("- Вставка в середину быстрее у LinkedList: узел вставляется за O(1),");
            Console.WriteLine("  а у List каждый раз сдвигаются элементы — O(n).");
            Console.WriteLine("- Доступ по индексу быстрее у List: прямой доступ O(1),");
            Console.WriteLine("  у LinkedList нужно пройти по цепочке от начала — O(n).");
            Console.WriteLine("Поэтому List — для случайного доступа, LinkedList — для частых вставок/удалений.");
        }
    }

}
