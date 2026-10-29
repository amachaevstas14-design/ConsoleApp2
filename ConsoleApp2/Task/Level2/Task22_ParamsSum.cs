using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task22_ParamsSum : ITask
    {
        public string Title { get; } = "params-метод + перегрузка";

        public string Description { get; } =
            "Написать метод суммы с параметром params и показать, как он вызывается с аргументами и с массивом.";

        public string[] Requirements { get; } = new[]
        {
            "Sum(params int[] numbers) — сумма произвольного числа чисел.",
        "Модификатор params позволяет вызывать метод с массивом: Sum(new[] { 1, 2, 3 }).",
        "Продемонстрировать вызовы Sum(1, 2, 3), Sum(new[] { 1, 2, 3 }) и Sum().",
        "Пустой вызов возвращает 0."
        };

        public void Run()
        {
            int sumOfArguments = Sum(1, 2, 3);
            int sumOfArray = Sum(new[] { 1, 2, 3 });
            int sumOfEmpty = Sum();

            Console.WriteLine($"Sum(1, 2, 3) = {sumOfArguments}");
            Console.WriteLine($"Sum(new[] {{ 1, 2, 3 }}) = {sumOfArray}");
            Console.WriteLine($"Sum() = {sumOfEmpty}");
            Console.WriteLine(
                $"Результаты первых двух вызовов совпадают: {sumOfArguments == sumOfArray}");
        }
        public static int Sum(params int[] numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            return sum;
        }
        
    }
}
