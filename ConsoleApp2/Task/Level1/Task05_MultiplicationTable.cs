using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task05_MultiplicationTable : ITask
    {
        private const int Size = 10;

        public string Title { get; } = "Таблица умножения 10х10";

       
        public string Description { get; } = "Вывести таблицу умножения от 1х1 до 10х10.";

        
        public string[] Requirements { get; } = new[]
        {
            "Использовать вложенные циклы for.",
        "Выровнять столбцы.",
        "Добавить заголовки строк и столбцов."
        };

        public void Run()
        {
            PrintHeader();

            for (int row = 1; row <= Size; row++)
            {
                Console.Write($"{row,3} |");

                for (int column = 1; column <= Size; column++)
                {
                    Console.Write($"{row * column,5}");
                }

                Console.WriteLine();
            }
        }

        private static void PrintHeader()
        {
            Console.Write("    |");

            for (int column = 1; column <= Size; column++)
            {
                Console.Write($"{column,5}");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', 5 + 5 * Size));
        }
    }
}
