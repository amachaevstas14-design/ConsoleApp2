using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task10_SortingComparison : ITask
    {
        private const int ArraySize = 10_000;

        public string Title { get; } = "Сортировка пузырьком + вставками";

        public string Description { get; } = "Отсортировать одинаковые массивы двумя алгоритмами и сравнить время выполнения.";

        public string[] Requirements { get; } = new[]
        {
            "Реализовать BubbleSort и InsertionSort отдельными методами.",
        "Оба — без LINQ и Array.Sort.",
        "Замерить время каждого через Stopwatch на массиве 10 000 элементов.",
        "Вывести оба времени."
        };

        public void Run()
        {
            int[] source = GenerateRandomArray(ArraySize);

            int[] bubbleArray = (int[])source.Clone();
            int[] insertionArray = (int[])source.Clone();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            BubbleSort(bubbleArray);
            stopwatch.Stop();
            long bubbleTicks = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            InsertionSort(insertionArray);
            stopwatch.Stop();
            long insertionTicks = stopwatch.ElapsedTicks;

            Console.WriteLine($"Размер массива: {ArraySize}");
            Console.WriteLine($"BubbleSort:    {bubbleTicks} тиков ({TimeSpan.FromTicks(bubbleTicks).TotalMilliseconds:F2} мс)");
            Console.WriteLine($"InsertionSort: {insertionTicks} тиков ({TimeSpan.FromTicks(insertionTicks).TotalMilliseconds:F2} мс)");
            Console.WriteLine(AreEqual(bubbleArray, insertionArray)
                ? "Оба массива отсортированы одинаково."
                : "Результаты сортировок различаются — что-то пошло не так.");
        }

        private static void BubbleSort(int[] numbers)
        {
            int n = numbers.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        (numbers[j], numbers[j + 1]) = (numbers[j + 1], numbers[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }
        private static void InsertionSort(int[] numbers)
        {
            int n = numbers.Length;

            for (int i = 1; i < n; i++)
            {
                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }

                numbers[j + 1] = key;
            }
        }

        private static int[] GenerateRandomArray(int size, int min = 1, int max = 100_000)
        {
            var random = new Random();
            int[] numbers = new int[size];

            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.Next(min, max + 1);
            }

            return numbers;
        }

        private static bool AreEqual(int[] left, int[] right)
        {
            if (left.Length != right.Length)
            {
                return false;
            }

            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
