using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2.Task.Level3
{
    public sealed class Task40_AsyncDownload : ITask
    {
        public string Title { get; } = "Асинхронная загрузка (async/await)";

        public string Description { get; } =
            "Метод DownloadAsync имитирует загрузку данных из интернета: задержка в 1 секунду " +
            "через Task.Delay, после чего возвращается «содержимое». Пустой или null-адрес " +
            "вызывает ArgumentException, который перехватывается через try/catch. " +
            "Задержка не блокирует поток — управление возвращается вызывающему коду, " +
            "а продолжение выполняется после завершения Task.Delay.";

        public string[] Requirements { get; } = new[]
        {
            "Метод DownloadAsync имеет подпись async Task<string> DownloadAsync(string url).",
        "Пустая строка или null перед url приводят к ArgumentException.",
        "Сетевая задержка имитируется await Task.Delay(1000).",
        "Ошибки обрабатываются через try/catch, программа не прерывается.",
        };

        public static async System.Threading.Tasks.Task<string> DownloadAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL не может быть пустым", nameof(url));
            }

            Console.WriteLine($"  [async] начинаю загружать: {url}");
            await System.Threading.Tasks.Task.Delay(1000);
            var content = $"[содержимое: {url}]";
            Console.WriteLine($"  [async] готово: {content}");
            return content;
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Console.WriteLine(Title);

            Console.WriteLine("1) Успешная загрузка:");
            var result = await DownloadAsync("https://example.com/article");
            Console.WriteLine($"  Результат: {result}");

            Console.WriteLine();
            Console.WriteLine("2) Ошибка: пустой URL:");
            try
            {
                await DownloadAsync("");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  Поймана ArgumentException: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("3) Ошибка: null вместо URL:");
            try
            {
                await DownloadAsync(null);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  Поймана ArgumentException: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Пояснение: ключевое слово async помечает метод, который может приостанавливаться на await. " +
                "await Task.Delay(1000) не блокирует поток — метод «сдаёт» управление, " +
                "а через одну секунду продолжается с того места, где был await. " +
                "Поэтому асинхронный код не держит поток занятый во время ожидания, " +
                "и приложение остаётся отзывчивым (например, интерфейс не «зависает» во время загрузки).");
        }

        public void Run() => RunAsync().GetAwaiter().GetResult();
    }

}
