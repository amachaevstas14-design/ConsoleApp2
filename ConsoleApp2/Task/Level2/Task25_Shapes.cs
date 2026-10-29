using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task25_Shapes : ITask
    {
        public string Title { get; } = "Интерфейс IShape + сортировка фигур";

        public string Description { get; } =
            "Реализовать интерфейс IShape для фигур и отсортировать список фигур по площади.";

        public string[] Requirements { get; } = new[]
        {
            "Интерфейс IShape с методами Area() и Perimeter().",
        "Классы Circle, Rectangle и Triangle реализуют IShape.",
        "Создать List<IShape> из 5 и более фигур.",
        "Отсортировать список по площади через List.Sort с лямбдой.",
        "Вывести фигуры с площадями до и после сортировки."
        };

        public void Run()
        {
            List<IShape> shapes = new List<IShape>
            {
                new Circle(3),
            new Rectangle(4, 5),
            new Triangle(3, 4, 5),
            new Circle(5),
            new Rectangle(2, 2),
            new Triangle(13, 14, 15)
            };

            Console.WriteLine("До сортировки:");

            foreach (IShape shape in shapes)
            {
                PrintShape(shape);
            }

            shapes.Sort((left, right) => left.Area().CompareTo(right.Area()));

            Console.WriteLine();
            Console.WriteLine("После сортировки по площади:");

            foreach (IShape shape in shapes)
            {
                PrintShape(shape);
            }
        }

        private static void PrintShape(IShape shape)
        {
            Console.WriteLine(
                $"{shape}: площадь = {shape.Area():F2}, периметр = {shape.Perimeter():F2}");
        }
    }
    public interface IShape
    {
        double Area();

        double Perimeter();
    }

    public sealed class Circle : IShape
    {
        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Радиус должен быть положительным.");
            }

            Radius = radius;
        }
        public double Radius { get; }

        public double Area() => Math.PI * Radius * Radius;

        public double Perimeter() => 2 * Math.PI * Radius;

        public override string ToString() => $"Круг (r = {Radius:F2})";
    }

    public sealed class Rectangle : IShape
    {
        public Rectangle(double width, double height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), width, "Сторона должна быть положительной.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), height, "Сторона должна быть положительной.");
            }

            Width = width;
            Height = height;
        }

        public double Width { get; }

        public double Height { get; }

        public double Area() => Width * Height;

        public double Perimeter() => 2 * (Width + Height);

        public override string ToString() => $"Прямоугольник ({Width:F0} x {Height:F0})";
    }

    public sealed class Triangle : IShape
    {
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(a), "Стороны треугольника должны быть положительными.");
            }

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                throw new ArgumentException(
                    "Указанные стороны не образуют треугольник.");
            }

            A = a;
            B = b;
            C = c;
        }

        public double A { get; }
        public double B { get; }
        public double C { get; }

        public double Area()
        {
            double s = (A + B + C) / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }

        public double Perimeter() => A + B + C;

        public override string ToString() => $"Треугольник ({A:F0}, {B:F0}, {C:F0})";
    }
}
