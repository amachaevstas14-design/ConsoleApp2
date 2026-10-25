using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task03_Calculator: ITask
    {
        public string Title { get; } = "Калькулятор с обработкой деления на ноль";

        public string Description { get; } = "Принимает два числа и оператор (+, -, *, /), выводит результат.";

        public string[] Requirements { get; } = new[]
        {
            "Оператор обрабатывать через switch.",
        "При делении на ноль — вывести сообщение об ошибке, не ронять приложение.",
        "Некорректный оператор — вывести «Неизвестная операция».",
        "Ввод чисел — через double.TryParse."
        };

        public void Run()
        {
            double first = ConsoleHelper.ReadDouble("Введите первое число:");
            double second = ConsoleHelper.ReadDouble("Введите второе число:");
            string operation = ReadOperation();

            double result;

            switch (operation)
            {
                case "+":
                    result = first + second;
                    break;

                    case "-":
                    result = first - second;
                    break;

                    case "*":
                    result = first * second;
                    break;

                    case "/":
                    if(second == 0)
                    {
                        Console.WriteLine("Ошибка: деление на ноль невозможно.");
                        return;
                    }

                    result = first / second;
                    break;

                    default:
                    Console.WriteLine("Неизвестная операция.");
                    return ;
            }
            Console.WriteLine($"Результат: {first} {operation} {second} = {result}");
        }

        private static string ReadOperation()
        {
            while (true)
            {
                string operation = ConsoleHelper.ReadLine("Выберите оператор (+ - * /): ");

                if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
                {
                    return operation;
                }
                Console.WriteLine("Неизвестная операция. Допустимы: + - * /.");
            }
        }
    }
}
