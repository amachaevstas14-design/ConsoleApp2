using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task33_LinqEmployees: ITask
    {
        public string Title { get; } = "LINQ: сотрудники (top-3, avg по отделу, фильтр)";

        public string Description { get; } =
            "LINQ-запросы: топ-3 по зарплате, средняя зарплата по отделам и фильтр по возрасту.";

        public string[] Requirements { get; } = new[]
        {
            "Класс Employee со свойствами Name, Age, Salary, Department.",
        "Список из 10 и более сотрудников.",
        "Топ-3 по зарплате (по убыванию) через OrderByDescending и Take.",
        "Средняя зарплата по каждому отделу через GroupBy и Average.",
        "Сотрудники старше 30 через Where."
        };

        private static readonly Employee[] Employees = new Employee[]
        {
            new Employee("Анна Смирнова", 28, 85_000, "Разработка"),
        new Employee("Борис Иванов", 34, 92_000, "Разработка"),
        new Employee("Вера Кузнецова", 41, 120_000, "Разработка"),
        new Employee("Глеб Петров", 25, 70_000, "Тестирование"),
        new Employee("Дарья Соколова", 33, 78_000, "Тестирование"),
        new Employee("Егор Волков", 29, 74_000, "Тестирование"),
        new Employee("Жанна Морозова", 38, 95_000, "Дизайн"),
        new Employee("Иван Павлов", 45, 130_000, "Дизайн"),
        new Employee("Кира Лебедева", 27, 65_000, "Дизайн"),
        new Employee("Лев Новиков", 36, 110_000, "Разработка"),
        new Employee("Марта Орлова", 31, 88_000, "Тестирование"),
        new Employee("Николай Фёдоров", 24, 60_000, "Дизайн")
        };
        public void Run()
        {
            var employees = Employees.ToList();

            Console.WriteLine($"Всего сотрудников: {employees.Count}");

            Console.WriteLine();
            Console.WriteLine("=== Топ-3 по зарплате ===");

            var topThree = employees
                .OrderByDescending(e => e.Salary)
                .Take(3)
                .ToList();

            for (int i = 0; i < topThree.Count; i++)
            {
                Employee employee = topThree[i];
                Console.WriteLine(
                    $"{i + 1}. {employee.Name}, {employee.Department} — {employee.Salary:N0}");
            }

            Console.WriteLine();
            Console.WriteLine("=== Средняя зарплата по отделам ===");

            var departmentAverages = employees
                .GroupBy(e => e.Department)
                .OrderBy(g => g.Key)
                .Select(g => new { Department = g.Key, Average = g.Average(e => e.Salary) })
                .ToList();

            foreach (var group in departmentAverages)
            {
                Console.WriteLine($"{group.Department}: {group.Average:N0}");
            }

            Console.WriteLine();
            Console.WriteLine("=== Сотрудники старше 30 ===");

            var seniorEmployees = employees.Where(e => e.Age > 30).ToList();

            foreach (Employee employee in seniorEmployees)
            {
                Console.WriteLine($"{employee.Name}, {employee.Age} лет, {employee.Department}");
            }
        }

        public sealed class Employee
        {
            public Employee(string name, int age, decimal salary, string department)
            {
                Name = name;
                Age = age;
                Salary = salary;
                Department = department;
            }

            public string Name { get; }
            public int Age { get; }
            public decimal Salary { get; }
            public string Department { get; }

            public override string ToString()
            {
                return $"{Name} ({Age}, {Department}, {Salary:N0})";
            }
        }

    }
}
