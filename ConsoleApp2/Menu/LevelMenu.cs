using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Common;

namespace ConsoleApp2.Menu
{
    public sealed class LevelMenu
    {
        public void Run(int level, string title)
        {
            while (true)
            {
                IReadOnlyList<TaskEntry> tasks = TaskRegistry.GetTasks(level);

                if (tasks.Count == 0)
                {
                    Console.WriteLine($"{title}\n");
                    Console.WriteLine("Для этого уровня задания пока не добавлены.");
                    Console.WriteLine();
                    Console.WriteLine("0. Назад");
                    Console.WriteLine();

                    if (ReadChoice(0, 0) == 0)
                    {
                        return;
                    }

                    continue;
                }

                Console.WriteLine(title);
                Console.WriteLine();

                foreach (TaskEntry entry in tasks)
                {
                    Console.WriteLine($"{entry.Number}. {entry.Factory().Title}");
                }

                Console.WriteLine();
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                int choice = ReadChoice(0, tasks.Count);

                if (choice == 0)
                {
                    return;
                }

                TaskEntry selected = tasks[choice - 1];
                ShowTask(selected.Factory(), selected.Number);
            }
        }
        private static void ShowTask(ITask task, int taskNumber)
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine($"Задание {taskNumber}. {task.Title}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Описание:");
            Console.WriteLine($"  {task.Description}");
            Console.WriteLine();
            Console.WriteLine("Требования:");

            foreach (string requirement in task.Requirements)
            {
                Console.WriteLine($"  - {requirement}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("[запуск решения...]");
            Console.WriteLine();

            task.Run();

            Console.WriteLine();
            Console.Write("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
        }

        private static int ReadChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("Выберите задание: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
                Console.WriteLine();
            }
        }
    }
}
