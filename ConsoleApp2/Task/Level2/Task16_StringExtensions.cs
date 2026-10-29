using ConsoleApp2.Common;
using ConsoleApp2.Task.Level1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    internal class Task16_StringExtensions : ITask
    {
        public string Title { get; } = "Методы расширения для string";

        public string Description { get; } = "Реализовать и продемонстрировать методы расширения для строк.";

        public string[] Requirements { get; } = new[]
        {
             "Реализовать методы расширения в статическом классе StringExtensions.",
        "Реализовать методы IsPalindrome(), WordCount() и ReverseString().",
        "Объявить методы как public static с параметром this string.",
        "Продемонстрировать вызов методов на нескольких строках."
        };

        public void Run()
        {
            string[] examples =
            {
               "А роза упала на лапу Азора",
               "Hello world",
               "abc"
            };
            
            foreach (string text in examples)
            {
                Console.WriteLine($"Исходная строка: {text}");
                Console.WriteLine($"Палиндром: {text.IsPalindrome()}");
                Console.WriteLine($"Количество слов: {text.WordCount().Values.Sum()}");
                Console.WriteLine($"Обратная строка: {text.ReversesString()}");
                Console.WriteLine();
            }
        }
    }

    public static class StringExtensions
    {
        public static bool IsPalindrome( this string text)
        {
            return Task12_Palindrome.IsPalindrome(text);
        }

        public static Dictionary<string, int> WordCount(this string text)
        {
            return Task13_WordCounter.CountWords(text);
        }

        public static string ReversesString(this string text)
        {
            return Task07_StringReversal.Reverse(text);
        }
    }
    
    
}
