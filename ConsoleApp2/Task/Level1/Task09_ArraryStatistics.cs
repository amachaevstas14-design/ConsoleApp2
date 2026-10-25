using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task09_ArrayStatistics : ITask
    {
        private const int ArraySize = 20;

        public string Title { get; } = "Массивы: min/max/sum/avg";

        public string Description { get; } = "Для массива случайных чисел найти минимум, максимум, сумму и среднее без LINQ.";

        public string[] Requirements { get; } = new[]
        {
            "Заполнить массив случайными числами 1–100, размер 20.",
        "Реализовать без LINQ (Min/Max/Sum/Average запрещены).",
        "Оформить 4 отдельных метода.",
        "Пустой массив — обработать, не падать."
        };

        public void Run()
        {
            int[] numbers = GenerateRandomArray(ArraySize);

            Console.WriteLine("Массив: " + string.Join(" ", numbers));
            Console.WriteLine();

            if (numbers.Length == 0)
            {
                Console.WriteLine("Массив пуст — статистика недоступна.");
                return;
            }

            Console.WriteLine($"Минимум:  {FindMin(numbers)}");
            Console.WriteLine($"Максимум: {FindMax(numbers)}");
            Console.WriteLine($"Сумма:    {Sum(numbers)}");
            Console.WriteLine($"Среднее:  {Average(numbers)}");
        }

        private static int[] GenerateRandomArray(int size, int min = 1, int max = 100)
        {
            var random = new Random();
            int[] numbers = new int[size];

            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.Next(min, max + 1);
            }

            return numbers;
        }

        private static int FindMin(int[] numbers)
        {
            EnsureNotEmpty(numbers);

            int min = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
            }

            return min;
        }

        private static int FindMax(int[] numbers)
        {
            EnsureNotEmpty(numbers);

            int max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        private static int Sum(int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        private static double Average(int[] numbers)
        {
            EnsureNotEmpty(numbers);
            return (double)Sum(numbers) / numbers.Length;
        }

        private static void EnsureNotEmpty(int[] numbers)
        {
            if (numbers.Length == 0)
            {
                throw new InvalidOperationException("Массив пуст.");
            }
        }
    }
}
