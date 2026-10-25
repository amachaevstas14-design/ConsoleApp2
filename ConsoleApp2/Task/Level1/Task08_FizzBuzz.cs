using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task08_FizzBuzz : ITask
    {
        private const int Limit = 100;

        public string Title { get; } = "FizzBuzz 1–100";

        public string Description { get; } = "Вывести числа от 1 до 100 с заменами Fizz/Buzz/FizzBuzz по правилам кратности.";

        public string[] Requirements { get; } = new[]
        {
            "Кратно 3 -> Fizz.",
        "Кратно 5 -> Buzz.",
        "Кратно 15 -> FizzBuzz.",
        "Иначе — само число.",
        "Порядок проверок: сначала 15, потом 3 и 5."
        };

        public void Run()
        {
            for (int number = 1; number <= Limit; number++)
            {
                Console.WriteLine(GetLabel(number));
            }
        }

        private static string GetLabel(int number)
        {
            if (number % 15 == 0)
            {
                return "FizzBuzz";
            }

            if (number % 3 == 0)
            {
                return "Fizz";
            }

            if (number % 5 == 0)
            {
                return "Buzz";
            }

            return number.ToString();
        }
    }
}
