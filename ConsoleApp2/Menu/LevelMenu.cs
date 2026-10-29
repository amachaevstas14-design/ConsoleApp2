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

                        if (ReadChoice(0, tasks) == 0)
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

                    int choice = ReadChoice(0, tasks);

                    if (choice == 0)
                    {
                        return;
                    }

                    TaskEntry selected = tasks.First(t => t.Number == choice);
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

            private static int ReadChoice(int backChoice, IReadOnlyList<TaskEntry> tasks)
            {
                int min = tasks[0].Number;
                int max = tasks[tasks.Count - 1].Number;

                while (true)
                {
                    Console.Write("Выберите задание: ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (choice == backChoice)
                            return backChoice;

                        foreach (TaskEntry entry in tasks)
                        {
                            if (entry.Number == choice)
                                return choice;
                        }
                    }

                    Console.WriteLine($"Ошибка: введите номер задания от {min} до {max} или 0 для возврата.");
                    Console.WriteLine();
                }
            }
        }
    }
