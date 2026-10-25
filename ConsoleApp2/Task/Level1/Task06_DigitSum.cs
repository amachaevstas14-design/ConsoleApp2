using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task06_DigitSum : ITask
    {
        public string Title { get; } = "Сумма цифр числа (строка + арифметика)";

        public string Description { get; } = "Найти сумму цифр введённого числа двумя способами и сравнить результаты.";

        public string[] Requirements { get; } = new[]
        {
            "Способ 1 (строка): ToString -> обход символов -> int.Parse каждой цифры.",
        "Способ 2 (арифметика): while (n > 0) { sum += n % 10; n /= 10; }.",
        "Оба способа оформить отдельными методами.",
        "Для отрицательных чисел брать модуль."
        };

        public void Run()
        {
            int number = ConsoleHelper.ReadInt("Введите целое число: ", int.MinValue, int.MaxValue);

            int byString = SumDigitsByString(number);
            int byArithmetic = SumDigitsByArithmetic(number);

            Console.WriteLine($"Способ 1 (строка):     {byString}");
            Console.WriteLine($"Способ 2 (арифметика): {byArithmetic}");
            Console.WriteLine(byString == byArithmetic
                ? "Оба способа дали одинаковый результат."
                : "Результаты различаются — что-то пошло не так.");
        }

        private static int SumDigitsByString(int number)
        {
            string digits = Math.Abs((long)number).ToString();
            int sum = 0;

            foreach (char symbol in digits)
            {
                sum += symbol - '0';
            }

            return sum;
        }

        private static int SumDigitsByArithmetic(int number)
        {
            long rest = Math.Abs((long)number);
            int sum = 0;

            while (rest > 0)
            {
                sum += (int)(rest % 10);
                rest /= 10;
            }

            return sum;
        }
    }
}
