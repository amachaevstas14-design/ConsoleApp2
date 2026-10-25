using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed  class Task02_Primitives: ITask
    {
        public string Title { get; } = "Переменные примитивных типов";

        public string Description { get; } = "Объявить переменные примитивных типов и вывести их значения и типы.";

        public string[] Requirements { get; } = new[]
        {
            "Объявить минимум: int, long, double, float, decimal, bool, char, string.",
        "Для каждой переменной вывести значение и GetType().Name.",
        "decimal — с суффиксом m, float — с суффиксом f."
        };

        public void Run()
        {
            int year = 2026;
            long worldPopulation = 8_200_000_000L;
            double avogadroNumber = 6.02214076e23;
            float lightSpeedKmPerSec = 299_792.458f;
            decimal bitcoinPrice = 67_999.99m;
            bool isLearningCSharp = true;
            char grade = 'A';
            string language = "C#";

            Show("year", year);
            Show("worldPopulation", worldPopulation);
            Show("avogadroNumber", avogadroNumber);
            Show("lightSpeedKmPerSec", lightSpeedKmPerSec);
            Show("bitcoinPrice", bitcoinPrice);
            Show("isLearningCSharp", isLearningCSharp);
            Show("grade", grade);
            Show("language", language);
        }

        private static void Show<T>(string name, T value)
        {
            string typeName = value == null ? "null" : value.GetType().Name;
            Console.WriteLine($"{name} = {value} ({typeName})");
        }

    }


}
