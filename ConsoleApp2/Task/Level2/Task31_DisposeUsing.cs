using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task31_DisposeUsing: ITask
    {
        public string Title { get; } = "IDisposable + using";

        public string Description { get; } =
            "Реализовать IDisposable для класса FileWriter и продемонстрировать using и using var.";

        public string[] Requirements { get; } = new[]
        {
            "Класс FileWriter с полем StreamWriter.",
        "Реализовать Dispose() — закрыть StreamWriter.",
        "Продемонстрировать использование через using.",
        "Продемонстрировать using-декларацию (using var)."
        };

        public void Run()
        {
            string path = Path.Combine(
                Path.GetTempPath(), "CSharpPracticum", "task31.txt");

            Console.WriteLine($"Файл: {path}");
            Console.WriteLine();

            Console.WriteLine("=== using: Dispose вызывается при выходе из блока ===");

            using (var writer = new FileWriter(path))
            {
                writer.WriteLine("Первая запись (через using)");
                writer.WriteLine("Вторая запись (через using)");
            }

            Console.WriteLine("Блок using завершён — файл закрыт.");

            Console.WriteLine();
            Console.WriteLine("=== Повторная запись не падает ===");

            using (var secondWriter = new FileWriter(path))
            {
                secondWriter.WriteLine("Третья запись (повторное открытие)");
            }

            Console.WriteLine("Второе открытие и запись прошли успешно.");

            Console.WriteLine();
            Console.WriteLine("=== using var: Dispose в конце области видимости ===");

            using (var thirdWriter = new FileWriter(path))
            {
                thirdWriter.WriteLine("Четвёртая запись (через using var)");
            }
            Console.WriteLine("using var — Dispose будет вызван автоматически в конце блока.");

            Console.WriteLine();

            string[] lines = File.ReadAllLines(path);

            Console.WriteLine("Содержимое файла после всех записей:");

            foreach (string line in lines)
            {
                Console.WriteLine($"  {line}");
            }
        }
    }

    public sealed class FileWriter : IDisposable
    {
        private StreamWriter _stream;

        public FileWriter(string path)
        {
            _stream = new StreamWriter(path, append: true);
        }
        public void WriteLine(string text)
        {
            if (_stream is null)
            {
                throw new ObjectDisposedException(nameof(FileWriter));
            }

            _stream.WriteLine(text);
        }
        public void Dispose()
        {
            _stream?.Dispose();
            _stream = null;
        }
    }
}

