using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Menu
{
    public sealed class MenuRunner
    {
        private const int ExitChoice = 4;

        private static readonly string[] _levelTitles = new[]
{
    "--- Уровень 1: Базовый ---",
    "--- Уровень 2: Средний ---",
    "--- Уровень 3: Продвинутый ---"
};

        private readonly LevelMenu _levelMenu;

    public MenuRunner(LevelMenu levelMenu)
    {
        _levelMenu = levelMenu ?? throw new ArgumentNullException(nameof(levelMenu));
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== Практикум по C# ===");
            Console.WriteLine("1. Уровень 1 — Базовый (задания 1–15)");
            Console.WriteLine("2. Уровень 2 — Средний (задания 16–35)");
            Console.WriteLine("3. Уровень 3 — Продвинутый (задания 36–50)");
            Console.WriteLine($"{ExitChoice}. Выход");
            Console.WriteLine();

            Console.Write("Выберите уровень: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > ExitChoice)
            {
                Console.WriteLine($"Ошибка: введите число от 1 до {ExitChoice}.");
                Console.WriteLine();
                continue;
            }

            if (choice == ExitChoice)
            {
                Console.WriteLine("Выход из программы.");
                return;
            }

            Console.Clear();
            _levelMenu.Run(choice, _levelTitles[choice - 1]);
        }
    }
    }
}
