using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task20_GenericMethods : ITask
    {
        
        public string Title { get; } = "Обобщения: Swap<T>, Max<T> с IComparable<T>";

        public string Description { get; } = "Написать обобщённые методы обмена значений и поиска максимума.";

        public string[] Requirements { get; } = new[]
        {
            "Swap<T>(ref T a, ref T b) меняет значения местами.",
        "Max<T>(T a, T b), где T : IComparable<T>, возвращает большее значение.",
        "Продемонстрировать работу методов с int, string и double.",
        "Swap не имеет ограничений типа, Max имеет ограничение IComparable<T>."
        };

        public void Run()
        {
            Console.WriteLine("=== Работа с int ===");

            int firstNumber = 10;
            int secondNumber = 20;

            Console.WriteLine($"До Swap: {firstNumber}, {secondNumber}");
            Swap(ref firstNumber, ref secondNumber);
            Console.WriteLine($"После Swap: {firstNumber}, {secondNumber}");
            Console.WriteLine($"Max: {Max(firstNumber, secondNumber)}");

            Console.WriteLine("\n=== Работа с string ===");

            string firstText = "Apple";
            string secondText = "Orange";

            Console.WriteLine($"До Swap: {firstText}, {secondText}");
            Swap(ref firstText, ref secondText);
            Console.WriteLine($"После Swap: {firstText}, {secondText}");
            Console.WriteLine($"Max: {Max(firstText, secondText)}");

            Console.WriteLine("\n=== Работа с double ===");

            double firstValue = 3.14;
            double secondValue = 2.71;

            Console.WriteLine($"До Swap: {firstValue}, {secondValue}");
            Swap(ref firstValue, ref secondValue);
            Console.WriteLine($"После Swap: {firstValue}, {secondValue}");
            Console.WriteLine($"Max: {Max(firstValue, secondValue)}");
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }

        public static T Max<T>(T a, T b)
            where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }
        
    }
}
