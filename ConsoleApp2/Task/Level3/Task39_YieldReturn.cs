using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task39_YieldReturn : ITask
    {
        public string Title { get; } = "Ленивая последовательность (yield return)";

        public string Description { get; } =
            "Генератор GetEvenNumbers возвращает IEnumerable<int> и выдает четные числа " +
            "через yield return. Значения не складываются в промежуточный список — они " +
            "вычисляются по одному, ровно в тот момент, когда потребитель просит следующее. " +
            "Ленивость показана: при раннем выходе из foreach генератор останавливается " +
            "и оставшиеся значения не вычисляются.";

        public string[] Requirements { get; } = new[]
        {
            "GetEvenNumbers(int max) возвращает IEnumerable<int> и использует yield return.",
        "Промежуточный список не создается — значения генерируются по одному.",
        "Внутри генератора печатается сообщение о каждом выданном значении — видно, что вызовы идут по одному.",
        "Показано полное прохождение и ранний выход (break) из foreach.",
        };

        public static IEnumerable<int> GetEvenNumbers(int max)
        {
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "max не может быть отрицательным");
            }

            for (var i = 1; i <= max; i++)
            {
                if (i % 2 != 0)
                {
                    continue;
                }

                Console.WriteLine($"    [генератор] вычисляю и выдаю {i}");
                yield return i;
            }
        }

        public void Run()
        {
            const int max = 10;

            Console.WriteLine(Title);

            Console.WriteLine($"1) Полное прохождение GetEvenNumbers({max}):");
            var consumed = 0;
            foreach (var n in GetEvenNumbers(max))
            {
                consumed++;
                Console.WriteLine($"    [потребитель] получил {n}");
            }
            Console.WriteLine($"    Потреблено значений: {consumed} (всего четных в 1..{max} — 5)");

            Console.WriteLine();
            Console.WriteLine($"2) Ранний выход: break после значения 6, генератор — GetEvenNumbers({max}):");
            foreach (var n in GetEvenNumbers(max))
            {
                Console.WriteLine($"    [потребитель] получил {n}");
                if (n == 6)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine(
                "Пояснение: yield return превращает метод в генератор — состояние цикла сохраняется между вызова MoveNext(). " +
                "Каждое значение вычисляется только тогда, когда потребитель запрашивает следующее (MoveNext), " +
                "промежуточный список не нужен. Поэтому при break после 6 генератор выдал только 2, 4 и 6 " +
                "и остановился — значения 8 и 10 не вычислялись вовсе. " +
                "Такие генераторы удобны для бесконечных и очень больших последовательностей.");
        }
    }
}
