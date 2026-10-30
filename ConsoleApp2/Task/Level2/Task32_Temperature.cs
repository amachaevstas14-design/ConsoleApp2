using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task32_Temperature : ITask
    {
        public string Title { get; } = "Свойства с валидацией (Temperature)";

        public string Description { get; } =
            "Класс Temperature с проверкой валидности значения в set и вычисляемым свойством Fahrenheit.";

        public string[] Requirements { get; } = new[]
        {
            "Свойство Celsius с проверкой в set: не ниже −273.15.",
        "При некорректном значении бросать ArgumentOutOfRangeException.",
        "Вычисляемое свойство Fahrenheit (только get).",
        "Продемонстрировать корректное и некорректное присваивание."
        };

        public void Run()
        {
            var temperature = new Temperature();

            temperature.Celsius = 25;

            Console.WriteLine($"Celsius = {temperature.Celsius}");
            Console.WriteLine($"Fahrenheit = {temperature.Fahrenheit}");

            Console.WriteLine();

            temperature.Celsius = -273.15;
            Console.WriteLine($"Celsius = {temperature.Celsius} (абсолютный ноль)");
            Console.WriteLine($"Fahrenheit = {temperature.Fahrenheit}");

            Console.WriteLine();

            Console.WriteLine("Попытка присвоить -300:");

            try
            {
                temperature.Celsius = -300;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }

            Console.WriteLine($"Celsius после неудачного присваивания: {temperature.Celsius}");
            Console.WriteLine("Значение осталось прежним — валидация работает.");
        }
    }
    public sealed class Temperature
    {
        private const double AbsoluteZero = -273.15;

        private double _celsius;

        public double Celsius
        {
            get => _celsius;

            set
            {
                if (value < AbsoluteZero)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value,
                        "Температура не может быть ниже абсолютного нуля (−273.15 °C).");
                }

                _celsius = value;
            }
        }

        public double Fahrenheit => Celsius * 9 / 5 + 32;

        public override string ToString()
        {
            return $"{Celsius} °C = {Fahrenheit} °F";
        }
    }

}

