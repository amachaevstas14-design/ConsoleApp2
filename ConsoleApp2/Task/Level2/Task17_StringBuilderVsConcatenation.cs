using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    internal sealed class Task17_StringBuilderVsConcatenation : ITask
    {
        private const int NumberCount = 10_000;

        public string Title { get; } = "StringBuilder vs конкатенация (замер)";

        public string Description { get; } =
            "Сравнить производительность конкатенации строк через оператор + и StringBuilder.";
        public string[] Requirements { get; } = new[]
        {
            "Собрать строку из 10 000 чисел двумя способами.",
        "Замерить время каждого способа через Stopwatch.",
        "Вывести оба времени и разницу между ними.",
        "Объяснить, почему StringBuilder может работать быстрее."
        };

        public void Run()
        {
            var stringBuilder = new StringBuilder(NumberCount);
            string concatenatedStirng = string.Empty;

            var stringBuilderStopwatch = Stopwatch.StartNew();

            for (int i = 0; i < NumberCount; i++)
            {
                stringBuilder.Append(i);
            }

            stringBuilderStopwatch.Stop();

            var concatenationStopwatch = Stopwatch.StartNew();

            for (int i = 0; i< NumberCount; i++)
            {
                concatenatedStirng += i;
            }

            stringBuilderStopwatch.Stop();

            string builderResult = stringBuilder.ToString();

            bool resultsAreEqual = builderResult == concatenatedStirng;

            Console.WriteLine($"Количество чисел: {NumberCount}");
            Console.WriteLine(
                $"StringBuilder: {stringBuilderStopwatch.Elapsed.TotalMilliseconds:F3} мс");
            Console.WriteLine(
                $"Конкатенация: {concatenationStopwatch.Elapsed.TotalMilliseconds:F3} мс");
            Console.WriteLine(
                $"Разница: {Math.Abs(stringBuilderStopwatch.Elapsed.TotalMilliseconds - concatenationStopwatch.Elapsed.TotalMilliseconds):F3} мс");
            Console.WriteLine($"Результаты совпадают: {resultsAreEqual}");
        }
    }
    
    
}
