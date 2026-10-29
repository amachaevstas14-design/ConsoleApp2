using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task12_Palindrome : ITask
    {
        public string Title { get; } = "Палиндром (без учёта регистра/пробелов)";

        public string Description { get; } = "Определить, читается ли строка одинаково в обе стороны, игнорируя регистр и небуквенные символы.";

        public string[] Requirements { get; } = new[]
        {
            "Игнорировать регистр (ToLower/ToUpper).",
        "Игнорировать пробелы и знаки пунктуации.",
        "Не использовать LINQ Reverse.",
        "Пустая строка и строка из одного символа — палиндром."
        };

        public void Run()
        {
            string input = ConsoleHelper.ReadLine("Введите строку: ");

            Console.WriteLine(IsPalindrome(input)
                ? "Это палиндром."
                : "Это не палиндром.");
        }

        public static bool IsPalindrome(string text)
        {
            string cleaned = ExtractLetters(text);

            int left = 0;
            int right = cleaned.Length - 1;

            while (left < right)
            {
                if (cleaned[left] != cleaned[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        private static string ExtractLetters(string text)
        {
            var letters = new List<char>(text.Length);

            foreach (char symbol in text)
            {
                if (char.IsLetter(symbol))
                {
                    letters.Add(char.ToLower(symbol));
                }
            }

            return new string(letters.ToArray());
        }
    }
}
