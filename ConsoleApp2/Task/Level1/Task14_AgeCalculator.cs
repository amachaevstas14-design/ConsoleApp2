using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task14_AgeCalculator : ITask
    {
        public string Title { get; } = "Возраст по дате рождения";

        public string Description { get; } = "Вычислить точный возраст по дате рождения: сколько полных лет, месяцев и дней прошло.";

        public string[] Requirements { get; } = new[]
        {
            "Ввод даты через DateTime.TryParse или TryParseExact.",
        "Учесть, что день рождения в этом году мог ещё не наступить.",
        "Вывести «X лет, Y месяцев, Z дней».",
        "Некорректная дата — сообщение об ошибке, не падать."
        };

        public void Run()
        {
            DateTime birthDate = ConsoleHelper.ReadDate("Введите дату рождения (дд.мм.гггг): ");
            DateTime today = DateTime.Today;

            if (birthDate > today)
            {
                Console.WriteLine("Ошибка: дата рождения находится в будущем.");
                return;
            }

            (int years, int months, int days) = CalculateAge(birthDate, today);

            Console.WriteLine($"Возраст: {years} лет, {months} месяцев, {days} дней");
        }

        private static (int Years, int Months, int Days) CalculateAge(DateTime birthDate, DateTime today)
        {
            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(
                    today.AddMonths(-1).Year,
                    today.AddMonths(-1).Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return (years, months, days);
        }
    }

}
