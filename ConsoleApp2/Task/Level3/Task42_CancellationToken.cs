using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task42_CancellationToken : ITask
    {
        private const int Steps = 10;
        private const int StepDelayMs = 500;
        private const int CancelAfterMs = 2000;

        public string Title { get; } = "Отмена операции (CancellationToken)";

        public string Description { get; } =
            "Длительная операция состоит из 10 шагов по 500 мс (всего ~5 секунд). " +
            "Через 2 секунды вызывается CancellationTokenSource.Cancel(), " +
            "и ожидающий Task.Delay(token) выбрасывает OperationCanceledException. " +
            "Операция прерывается примерно на 5-м шаге, а исключение корректно обрабатывается " +
            "через try/catch/finally.";

        public string[] Requirements { get; } = new[]
        {
            "LongOperationAsync принимает CancellationToken и выполняет 10 шагов по await Task.Delay(500, token).",
        "Отмена отправляется через CancellationTokenSource.Cancel() через 2 секунды.",
        "OperationCanceledException перехватывается try/catch, в finally — освобождение ресурсов.",
        "Вывод: сколько шагов успело выполниться и что операция отменена.",
        };

        public static async System.Threading.Tasks.Task LongOperationAsync(CancellationToken cancellationToken)
        {
            for (var i = 1; i <= Steps; i++)
            {
                Console.WriteLine($"  [операция] шаг {i}/{Steps}, работаю {StepDelayMs} мс ...");
                await System.Threading.Tasks.Task.Delay(StepDelayMs, cancellationToken);
                Console.WriteLine($"  [операция] шаг {i}/{Steps} завершён");
            }

            Console.WriteLine($"  [операция] все {Steps} шагов выполнены");
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Console.WriteLine(Title);
            Console.WriteLine($"Операция: {Steps} шагов по {StepDelayMs} мс (всего ~{Steps * StepDelayMs / 1000.0:0.#} с)");
            Console.WriteLine($"Отмена будет отправлена через {CancelAfterMs / 1000.0:0.#} с");

            var cts = new CancellationTokenSource();
            var operation = LongOperationAsync(cts.Token);

            await System.Threading.Tasks.Task.Delay(CancelAfterMs);
            Console.WriteLine("  [демо] отправляю сигнал отмены: cts.Cancel()");
            cts.Cancel();

            try
            {
                await operation;
                Console.WriteLine("Операция завершилась полностью без отмены");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("операция отменена");
            }
            finally
            {
                Console.WriteLine("  finally: освобождаю ресурсы (закрываю соединения, файлы)");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Пояснение: токен передается внутрь операции, и каждый await Task.Delay(token) " +
                "проверяет флаг отмены. После cts.Cancel() зависший Task.Delay сразу выбрасывает " +
                "OperationCanceledException — операция прерывается на ~2-й секунде вместо того, " +
                "чтобы тянуться все 5 секунд. finally-блок гарантирует освобождение ресурсов, " +
                "даже если работа была прервана. Так пользователь может в любой момент прервать " +
                "долгую операцию, например загрузку или вычисление.");
        }

        public void Run() => RunAsync().GetAwaiter().GetResult();
    }
}
