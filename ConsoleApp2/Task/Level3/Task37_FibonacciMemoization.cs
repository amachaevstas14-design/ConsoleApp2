using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task37_FibonacciMemoization : ITask
    {
        private const int N = 35;

        public string Title { get; } = "Рекурсия и мемоизация (Fibonacci)";

        public string Description { get; } =
            "Сравниваются два способа вычислить n-е число Фибоначчи: наивная рекурсия " +
            "с экспоненциальной сложностью и рекурсия с мемоизацией через словарь. " +
            "Оба варианта измеряются через Stopwatch, показывается выигрыш в скорости.";

        public string[] Requirements { get; } = new[]
        {
            "FibonacciRecursive — наивная рекурсия F(n) = F(n-1) + F(n-2) без кэширования.",
        "FibonacciMemoized(n, memo) — рекурсия с мемоизацией в Dictionary<int, long>.",
        "Замер времени обоих вызовов через Stopwatch.",
        "Печатается скорость (число Фибоначчи), время каждого вызова и кратность ускорения.",
        };

        public static long FibonacciRecursive(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n не может быть отрицательным");
            }
            if (n < 2)
            {
                return n;
            }

            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        public static long FibonacciMemoized(int n, Dictionary<int, long> memo)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n не может быть отрицательным");
            }
            if (n < 2)
            {
                return n;
            }
            if (memo.TryGetValue(n, out var cached))
            {
                return cached;
            }

            var value = FibonacciMemoized(n - 1, memo) + FibonacciMemoized(n - 2, memo);
            memo[n] = value;
            return value;
        }

        public void Run()
        {
            Console.WriteLine(Title);

            var recursiveTime = TimeSpan.Zero;
            var memoizedTime = TimeSpan.Zero;

            long recursiveValue;
            long memoizedValue;

            var recursiveStopwatch = Stopwatch.StartNew();
            recursiveValue = FibonacciRecursive(N);
            recursiveStopwatch.Stop();
            recursiveTime = recursiveStopwatch.Elapsed;

            var memo = new Dictionary<int, long>();
            var memoizedStopwatch = Stopwatch.StartNew();
            memoizedValue = FibonacciMemoized(N, memo);
            memoizedStopwatch.Stop();
            memoizedTime = memoizedStopwatch.Elapsed;

            Console.WriteLine($"F({N}) = {recursiveValue}");
            Console.WriteLine($"Наивная рекурсия:    {recursiveTime.TotalSeconds:0.000} s");
            Console.WriteLine($"Мемоизированная:     {memoizedTime.TotalSeconds:0.000} s");

            var speedup = memoizedTime.TotalMilliseconds > 0
                ? recursiveTime.TotalMilliseconds / memoizedTime.TotalMilliseconds
                : recursiveTime.TotalMilliseconds > 0 ? double.PositiveInfinity : 1.0;

            Console.WriteLine($"Ускорение: x{speedup:0.#}");
            Console.WriteLine($"Значения совпадают: {(recursiveValue == memoizedValue ? "да" : "нет")}");
            Console.WriteLine(
                "Пояснение: наивная рекурсия считает одно и то же многократно (F(35) — десятки миллионов вызовов). " +
                "Мемоизация сохраняет уже вычисленные значения в словарь, и каждое число считается один раз — " +
                "сложность падает с экспоненциальной до линейной.");
        }
    }

}
