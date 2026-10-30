using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed  class Task27_StudentComparers: ITask
    {
        public string Title { get; } = "IComparable / IComparer<T> для Student";

        public string Description { get; } =
            "Научить класс Student сортироваться по среднему баллу и по имени.";

        public string[] Requirements { get; } = new string[]
        {
            "Класс Student со свойствами Name и AverageGrade.",
        "Реализовать IComparable<Student> — сортировка по среднему баллу (по убыванию).",
        "Реализовать IComparer<Student> ByNameComparer — сортировка по имени.",
        "Продемонстрировать обе сортировки на List<Student>."
        };

        public void Run()
        {
            List<Student> students = new List<Student>
            {
                new Student("Василий", 3.5),
            new Student("Анна", 4.8),
            new Student("Пётр", 4.8),
            new Student("Мария", 2.9),
            new Student("Дмитрий", 4.2)
            };

            Console.WriteLine("Исходный список:");

            foreach (Student student in students)
            {
                Console.WriteLine($"  {student}");
            }

            Console.WriteLine();

            List<Student> byGrade = students.ToList();
            byGrade.Sort();

            Console.WriteLine("Сортировка по среднему баллу (по убыванию):");

            foreach (Student student in byGrade)
            {
                Console.WriteLine($"  {student}");
            }

            Console.WriteLine();

            List<Student> byName = students.ToList();
            byName.Sort(new ByNameComparer());

            Console.WriteLine("Сортировка по имени:");

            foreach (Student student in byName)
            {
                Console.WriteLine($"  {student}");
            }
        }
    }
    public sealed class Student : IComparable<Student>
    {
        public Student(string name, double averageGrade)
        {
            Name = name;
            AverageGrade = averageGrade;
        }

        public string Name { get; }

        public double AverageGrade { get; }

        public int CompareTo(Student other)
        {
            if (other is null)
            {
                return 1;
            }

            int gradeComparison = other.AverageGrade.CompareTo(AverageGrade);
            return gradeComparison != 0
                ? gradeComparison
                : string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
        public override string ToString() => $"{Name} (средний балл: {AverageGrade:F1})";
    }

    public sealed class ByNameComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x is null)
            {
                return y is null ? 0 : 1;
            }

            if (y is null)
            {
                return -1;
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
        
    }
}

