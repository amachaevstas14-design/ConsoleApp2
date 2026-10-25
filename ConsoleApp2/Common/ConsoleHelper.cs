using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Common
{
    public static class ConsoleHelper
    {
        public static string ReadLine(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input.Length > 0)
                {
                    return input;
                }
                Console.WriteLine("Ошибка: введите непуствую строку");
            }
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                if(int.TryParse(ReadLine(prompt), out int value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Ошибка: введите целое число от {min} до {max}.");
            }
        }

        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                if(double.TryParse(ReadLine(prompt),out double value))
                {
                    return value;
                }
                Console.WriteLine("Ошибка: введите число.");
            }
        }

        public static DateTime ReadDate(string prompt)
        {
            while(true)
            {
                string input = ReadLine(prompt);

                if(DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime value) ||
                DateTime.TryParse(input, out value))
                {
                    return value;
                }
                Console.WriteLine("Ошибка: введите дату в формате дд.мм.гггг.");
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
