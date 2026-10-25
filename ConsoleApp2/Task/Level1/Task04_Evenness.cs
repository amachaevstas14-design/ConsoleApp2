using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task04_Evenness: ITask
    {
        public string Title { get; } = "Чётность числа (два способа)";

        public string Description { get; } = "Определить, чётное ли введённое число, двумя независимыми способами.";

        public string[] Requirements { get; } = new[]
        {
            "Способ 1: через остаток от деления на 2 (% 2 == 0).",
        "Способ 2: через тернарный оператор ? :.",
        "Оба способа реализовать отдельными методами.",
        "Вывести результат обоих способов."
        };

        public void Run()
        {
            int number = ConsoleHelper.ReadInt("Введите целое число: ", int.MinValue, int.MaxValue);

            Console.WriteLine($"Способ 1 (остаток от деления): {Describe(IsEvenByRemainder(number))}");
            Console.WriteLine($"Способ 2 (тернарный оператор): {Describe(IsEvenByTernary(number))}");
        }

        private static bool IsEvenByRemainder(int number)
        {
            return number % 2 == 0;
        }

        private static bool IsEvenByTernary(int number)
        {
            bool isEven = number % 2 == 0 ? true : false;
            return isEven;
        }

        private static string Describe(bool isEven)
        {
            return isEven ? "чётное" : "нечётное";
        }
    }
}
