using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task01_HelloWorld : ITask
    {
        public string Title { get; } = "Hello, World + текущая дата";

        public string Description { get; } = "Вывести приветствие и текущую дату.";

        public string[] Requirements { get; } = new[]
        {
            "Использовать Console.WriteLine.",
        "Дату получить через DateTime.Now, отформатировать как dd.MM.yyyy.",
        "Вывести две строки: приветствие и дату."
        };

        public void Run()
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Сегодня: {DateTime.Now:dd.MM.yyyy}");
        }
    }
    
    
}
