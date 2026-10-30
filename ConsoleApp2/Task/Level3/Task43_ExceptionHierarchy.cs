using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task43_ExceptionHierarchy : ITask
    {
        private static readonly string[] Scenarios = new[] { "format", "division", "custom" };

        public string Title { get; } = "Иерархия исключений и порядок catch";

        public string Description { get; } =
            "Три сценария бросают разные исключения: FormatException (int.Parse(\"abc\")), " +
            "DivideByZeroException (деление 10 на 0) и собственное CustomException. " +
            "Блоки catch расставлены от конкретного к общему, а последний — запасной " +
            "catch (Exception). finally выполняется всегда, независимо от исхода.";

        public string[] Requirements { get; } = new[]
        {
            "Сценарий format: int.Parse(\"abc\") бросает FormatException.",
        "Сценарий division: деление 10 / 0 бросает DivideByZeroException.",
        "Сценарий custom: собственное исключение CustomException : Exception.",
        "Порядок catch: сначала конкретные типы, в конце общий Exception.",
        "finally выводит «завершено» в любом случае."
        };

        private void Demonstrate(string scenario)
        {
            try
            {
                switch (scenario)
                {
                    case "format":
                        _ = int.Parse("abc");
                        break;

                    case "division":
                        ThrowDivideByZero();
                        break;

                    case "custom":
                        throw new CustomException("Код пользователя 42 не найден в каталоге");

                    default:
                        throw new ArgumentException($"Неизвестный сценарий: {scenario}", nameof(scenario));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"  Поймано FormatException: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"  Поймано DivideByZeroException: {ex.Message}");
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"  Поймано CustomException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Поймано {ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("  finally: завершено");
            }
        }
        public static void ThrowDivideByZero()
        {
            var divisor = 0;
            _ = 10 / divisor;
        }

        public void Run()
        {
            Console.WriteLine(Title);

            foreach (var scenario in Scenarios)
            {
                Console.WriteLine($"Сценарий «{scenario}»:");
                Demonstrate(scenario);
                Console.WriteLine();
            }

            Console.WriteLine(
                "Пояснение: все исключения наследуются от System.Exception, и порядок блоков " +
                "catch идёт сверху вниз — от самых конкретных типов к общим. Если бы catch (Exception) " +
                "стоял первым, он бы перехватывал все исключения, и блоки для FormatException, " +
                "DivideByZeroException и CustomException никогда не работали бы. finally выполняется " +
                "всегда — и при нормальном завершении, и при перехваченном исключении, поэтому в нём " +
                "выполняют гарантированное освобождение ресурсов.");
        }
    }
    public sealed class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }

}
