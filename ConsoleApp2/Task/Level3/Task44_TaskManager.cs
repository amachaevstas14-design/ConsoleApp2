using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
  

    namespace ConsoleApp2.Task.Level3
    {
        public sealed class Task44_TaskManager : ITask
        {
            public string Title { get; } = "Менеджер задач (class + репозиторий + сервис)";

            public string Description { get; } =
                "Менеджер задач состоит из трёх слоёв: class TaskItem как модель, " +
                "TaskRepository — хранение списка в tasks.json через System.Text.Json, " +
                "TaskService — бизнес-логика (добавление, список, отметка выполнения, удаление). " +
                "Пользователь работает через консольное меню, после каждого изменения " +
                "список сохраняется в tasks.json в корне проекта, а ID новой задачи " +
                "равно максимальному существующему + 1.";

            public string[] Requirements { get; } = new[]
            {
            "class TaskItem(int Id, string Title, bool IsDone).",
            "TaskRepository: запись и чтение списка через System.Text.Json; отсутствующий или повреждённый файл — как пустой список.",
            "TaskService: добавление (id = максимум существующих + 1), список, отметка выполнения, удаление; после каждого изменения файл сохраняется.",
            "Интерактивное меню: 1 — добавить, 2 — список, 3 — отметить выполненной, 4 — удалить, 0 — выход.",
            "Файл tasks.json создаётся в корне проекта (относительно расположения сборки)."
        };

            public void Run()
            {
                Console.WriteLine(Title);

                var repository = new TaskRepository(TaskRepository.DefaultFilePath);
                var service = new TaskService(repository);
                Console.WriteLine($"Файл хранения: {repository.FilePath}");
                Console.WriteLine($"Загружено задач: {service.ListTasks().Count}");

                while (true)
                {
                    PrintMenu();
                    var choice = ConsoleHelper.ReadInt("Выберите пункт (0-4): ", 0, 4);

                    switch (choice)
                    {
                        case 1:
                            AddTask(service);
                            break;

                        case 2:
                            PrintTasks(service);
                            break;

                        case 3:
                            MarkDone(service);
                            break;

                        case 4:
                            DeleteTask(service);
                            break;

                        case 0:
                            Console.WriteLine("Выход из менеджера задач.");
                            return;
                    }

                    Console.WriteLine();
                }
            }

            private static void PrintMenu()
            {
                Console.WriteLine();
                Console.WriteLine("Меню менеджера задач:");
                Console.WriteLine("  1 - добавить задачу");
                Console.WriteLine("  2 - показать список задач");
                Console.WriteLine("  3 - отметить задачу как выполненную");
                Console.WriteLine("  4 - удалить задачу");
                Console.WriteLine("  0 - выход");
            }

            private static void AddTask(TaskService service)
            {
                var title = ConsoleHelper.ReadLine("Введите название задачи: ");
                var item = service.AddTask(title);
                Console.WriteLine($"Добавлена задача №{item.Id}: «{item.Title}»");
            }

            private static void PrintTasks(TaskService service)
            {
                var items = service.ListTasks();
                if (items.Count == 0)
                {
                    Console.WriteLine("Список задач пуст.");
                    return;
                }

                var done = items.Count(t => t.IsDone);
                Console.WriteLine($"Всего задач: {items.Count}, выполнено: {done}");
                foreach (var item in items.OrderBy(t => t.Id))
                {
                    var mark = item.IsDone ? "✓" : "·";
                    Console.WriteLine($"  [{item.Id}] {mark} {item.Title}");
                }
            }

            private static void MarkDone(TaskService service)
            {
                var items = service.ListTasks();
                if (items.Count == 0)
                {
                    Console.WriteLine("Список задач пуст — нечего отмечать.");
                    return;
                }

                var maxId = items.Max(t => t.Id);
                var id = ConsoleHelper.ReadInt($"Введите ID задачи (1-{maxId}): ", 1, maxId);
                var item = items.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    Console.WriteLine($"Задача с ID {id} не найдена.");
                    return;
                }
                if (item.IsDone)
                {
                    Console.WriteLine($"Задача №{id} уже отмечена как выполненная.");
                    return;
                }

                service.MarkDone(id);
                Console.WriteLine($"Задача №{id} «{item.Title}» отмечена как выполненная.");
            }

            private static void DeleteTask(TaskService service)
            {
                var items = service.ListTasks();
                if (items.Count == 0)
                {
                    Console.WriteLine("Список задач пуст — нечего удалять.");
                    return;
                }

                var maxId = items.Max(t => t.Id);
                var id = ConsoleHelper.ReadInt($"Введите ID задачи (1-{maxId}): ", 1, maxId);
                var item = items.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    Console.WriteLine($"Задача с ID {id} не найдена.");
                    return;
                }

                service.DeleteTask(id);
                Console.WriteLine($"Задача №{id} «{item.Title}» удалена.");
            }
        }

        public sealed class TaskItem : IEquatable<TaskItem>
        {
            public TaskItem(int id, string title, bool isDone)
            {
                Id = id;
                Title = title;
                IsDone = isDone;
            }

            public int Id { get; }
            public string Title { get; }
            public bool IsDone { get; }

            public bool Equals(TaskItem other)
            {
                return other != null
                    && Id == other.Id
                    && Title == other.Title
                    && IsDone == other.IsDone;
            }
            public override bool Equals(object obj) => obj is TaskItem other && Equals(other);

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 31 + Id;
                    hash = hash * 31 + (Title?.GetHashCode() ?? 0);
                    hash = hash * 31 + IsDone.GetHashCode();
                    return hash;
                }
            }
            public override string ToString() => $"TaskItem({Id}, {Title}, {IsDone})";
        }

        public sealed class TaskRepository
        {
            private readonly string _filePath;
            public static string DefaultFilePath =>
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "tasks.txt"));

            public TaskRepository(string filePath)
            {
                _filePath = filePath;
            }
            public string FilePath => _filePath;

            public List<TaskItem> Load()
            {
                var items = new List<TaskItem>();

                if (!File.Exists(_filePath))
                {
                    return items;
                }

                try
                {
                    foreach (var line in File.ReadAllLines(_filePath))
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }


                        var parts = line.Split('|');
                        if (parts.Length != 3)
                        {
                            continue;
                        }

                        if (!int.TryParse(parts[0], out int id))
                        {
                            continue;
                        }

                        if (!bool.TryParse(parts[2], out bool isDone))
                        {
                            continue;
                        }

                        items.Add(new TaskItem(id, parts[1], isDone));
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Внимание: файл повреждён, начинаю с пустого списка.");
                    return new List<TaskItem>();
                }

                return items;
            }
            public void Save(IEnumerable<TaskItem> items)
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (directory != null && !string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var lines = new List<string>();
                foreach (var item in items)
                {
                    lines.Add($"{item.Id}|{item.Title}|{item.IsDone}");
                }

                File.WriteAllLines(_filePath, lines);
            }
        }
        public sealed class TaskService
        {
            private readonly TaskRepository _repository;

            public TaskService(TaskRepository repository)
            {
                _repository = repository;
            }

            public TaskItem AddTask(string title)
            {
                var items = _repository.Load();
                var nextId = items.Count == 0 ? 1 : items.Max(t => t.Id) + 1;

                var item = new TaskItem(nextId, title, false);
                items.Add(item);
                _repository.Save(items);
                return item;
            }

            public IReadOnlyList<TaskItem> ListTasks()
            {
                return _repository.Load();
            }

            public bool MarkDone(int id)
            {
                var items = _repository.Load();
                var index = items.FindIndex(t => t.Id == id);
                if (index < 0 || items[index].IsDone)
                {
                    return false;
                }

               
                var old = items[index];
                items[index] = new TaskItem(old.Id, old.Title, true);
                _repository.Save(items);
                return true;
            }

            public bool DeleteTask(int id)
            {
                var items = _repository.Load();
                var index = items.FindIndex(t => t.Id == id);
                if (index < 0)
                {
                    return false;
                }

                items.RemoveAt(index);
                _repository.Save(items);
                return true;
            }
        }
    }
}
