using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task28_PointEquality: ITask
    {
        public string Title { get; } = "Equals / GetHashCode для Point";

        public string Description { get; } =
            "Переопределить Equals и GetHashCode для точки и проверить работу с HashSet.";

        public string[] Requirements { get; } = new string[]
        {
            "Переопределить Equals(object) и GetHashCode().",
        "Реализовать IEquatable<Point>.",
        "Проверить, что две точки с одинаковыми координатами равны.",
        "Проверить, что добавление дубликата в HashSet<Point> не увеличивает Count."
        };

        public void Run()
        {
            Point first = new Point(1, 2);
            Point second = new Point(1, 2);
            Point third = new Point(3, 4);

            Console.WriteLine($"first = {first}");
            Console.WriteLine($"second = {second}");
            Console.WriteLine($"third = {third}");
            Console.WriteLine($"first.Equals(second): {first.Equals(second)}");
            Console.WriteLine($"first.Equals(third): {first.Equals(third)}");
            Console.WriteLine($"GetHashCode(first) = {first.GetHashCode()}");
            Console.WriteLine($"GetHashCode(second) = {second.GetHashCode()}");
            Console.WriteLine(
                $"GetHashCode равны: {first.GetHashCode() == second.GetHashCode()}");

            Console.WriteLine();
            Console.WriteLine("Работа с HashSet<Point>:");

            var set = new HashSet<Point>();
            bool addedFirst = set.Add(first);
            bool addedSecond = set.Add(second);
            bool addedThird = set.Add(third);

            Console.WriteLine($"Добавлена first (новый элемент): {addedFirst}, Count = {set.Count}");
            Console.WriteLine($"Добавлена second (дубликат): {addedSecond}, Count = {set.Count}");
            Console.WriteLine($"Добавлена third (новый элемент): {addedThird}, Count = {set.Count}");
        }
    }
    public sealed class Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point other)
        {
            return other != null && X == other.X && Y == other.Y;
        }
        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
        
    }
}

