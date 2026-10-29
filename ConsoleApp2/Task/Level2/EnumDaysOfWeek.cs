using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task21_EnumDaysOfWeek : ITask
    {
        public string Title { get; } = "Enum: дни недели";

        public string Description { get; } =
            "Описать дни недели через enum и определить тип дня.";

        public string[] Requirements { get; } = new[]
        {
            "Объявить перечисление DayOfWeek со всеми днями недели.",
        "Реализовать IsWeekend(DayOfWeek day) для проверки выходных.",
        "Реализовать GetRussianName(DayOfWeek day) для получения русского названия.",
        "Продемонстрировать работу методов для всех семи дней."
        };

        public void Run()
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                string name = GetRussianName(day);
                bool isWeekend = IsWeekend(day);

                Console.WriteLine(
                    $"{name}: {(isWeekend ? "выходной" : "рабочий день")}");
            }
        }

        public static bool IsWeekend(DayOfWeek day)
        {
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        public static string GetRussianName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Понедельник";
                case DayOfWeek.Tuesday: return "Вторник";
                case DayOfWeek.Wednesday: return "Среда";
                case DayOfWeek.Thursday: return "Четверг";
                case DayOfWeek.Friday: return "Пятница";
                case DayOfWeek.Saturday: return "Суббота";
                case DayOfWeek.Sunday: return "Воскресенье";
                default:
                    throw new InvalidOperationException("Неизвестный день недели.");
            }
        }
    }

    public enum DayOfWeek
    {
        Monday,

        Tuesday,

        Wednesday,

        Thursday,

        Friday,

        Saturday,

        Sunday
    }

}
