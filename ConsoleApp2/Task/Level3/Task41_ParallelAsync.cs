using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task41_ParallelAsync : ITask
    {
        private const int TaskCount = 10;
        private const int DelayMs = 500;

        public string Title { get; } = "Последовательный vs параллельный async";

        public string Description { get; } =
            "Каждая из 10 асинхронных задач работает 500 мс. При последовательном запуске " +
            "каждая следующая начинается после завершения предыдущей — в сумме около 5 секунд. " +
            "При параллельном запуске через Task.WhenAll все 10 задач стартуют сразу — " +
            "общее время равно времени одной задачи (около 500 мс).";

        public string[] Requirements { get; } = new[]
        {
            "WorkAsync(int id) ждёт 500 мс через Task.Delay и возвращает id.",
        "Последовательный цикл: каждый await ждёт завершения предыдущей задачи.",
        "Параллельный запуск: все задачи стартуют сразу, сбор через Task.WhenAll.",
        "Время обоих вариантов измеряется Stopwatch, результаты сравниваются.",
        };

        public static async System.Threading.Tasks.Task<int> WorkAsync(int id)
        {
            await System.Threading.Tasks.Task.Delay(DelayMs);
            return id;
        }

        public static async Task<List<int>> RunSequentialAsync(int count)
        {
            var results = new List<int>();
            for (var id = 1; id <= count; id++)
            {
                results.Add(await WorkAsync(id));
            }
            return results;
        }

        public static async Task<List<int>> RunParallelAsync(int count)
        {
            var tasks = new Task<int>[count];
            for (var id = 1; id <= count; id++)
            {
                tasks[id - 1] = WorkAsync(id);
            }

            var results = await System.Threading.Tasks.Task.WhenAll(tasks);
            return results.ToList();
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Console.WriteLine(Title);
            Console.WriteLine($"Задач: {TaskCount}, каждая работает {DelayMs} мс");

            var sequentialStopwatch = Stopwatch.StartNew();
            var sequentialResults = await RunSequentialAsync(TaskCount);
            sequentialStopwatch.Stop();

            var parallelStopwatch = Stopwatch.StartNew();
            var parallelResults = await RunParallelAsync(TaskCount);
            parallelStopwatch.Stop();

            Console.WriteLine($"Последовательно:    {sequentialStopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine($"Параллельно:       {parallelStopwatch.ElapsedMilliseconds} мс");

            var speedup = parallelStopwatch.ElapsedMilliseconds > 0
                ? sequentialStopwatch.ElapsedMilliseconds / parallelStopwatch.ElapsedMilliseconds
                : 1.0;
            Console.WriteLine($"Ускорение: x{speedup:0.#}");
            Console.WriteLine($"Результаты совпадают: {(sequentialResults.SequenceEqual(parallelResults) ? "да" : "нет")}");

            Console.WriteLine();
            Console.WriteLine(
                "Пояснение: в последовательном цикле await ждёт, пока задача завершится, " +
                "и только потом начинается следующая — 10 задач × 500 мс = ~5000 мс. " +
                "Вызов WorkAsync без await сразу запускает задачу в фоне, и все 10 стартуют разом; " +
                "Task.WhenAll ждёт завершения всех сразу, поэтому общее время ≈ 500 мс. " +
                "Параллелизм асинхронных операций — главный выигрыш async/await.");
        }

        public void Run() => RunAsync().GetAwaiter().GetResult();
    }
}
