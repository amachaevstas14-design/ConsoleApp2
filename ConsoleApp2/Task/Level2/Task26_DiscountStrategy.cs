using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task26_DiscountStrategy : ITask
    {
        private const decimal BasePrice = 1000m;

        public string Title { get; } = "Паттерн «Стратегия» (скидки)";

        public string Description { get; } =
            "Реализовать калькулятор скидок через паттерн «Стратегия».";

        public string[] Requirements { get; } = new[]
        {
            "Интерфейс IDiscountStrategy с методом Apply(decimal price).",
        "Реализации: NoDiscount, VipDiscount (10%) и PromoDiscount (20%).",
        "Класс PriceCalculator принимает стратегию в конструкторе.",
        "Продемонстрировать расчёт цены с каждой стратегией."
        };

        public void Run()
        {
            IDiscountStrategy[] strategies = new IDiscountStrategy[]
            {
                new NoDiscount(),
            new VipDiscount(),
            new PromoDiscount()
            };

            foreach (IDiscountStrategy strategy in strategies)
            {
                var calculator = new PriceCalculator(strategy);
                decimal finalPrice = calculator.Calculate(BasePrice);

                Console.WriteLine($"Базовая цена: {BasePrice}");
                Console.WriteLine($"Стратегия: {strategy.Name}");
                Console.WriteLine($"Итоговая цена: {finalPrice}");
                Console.WriteLine();
            }
        }
    }

    public interface IDiscountStrategy
    {
        string Name { get; }

        decimal Apply(decimal price);
    }
    public sealed class NoDiscount : IDiscountStrategy
    {
        public string Name => "Без скидки";

        public decimal Apply(decimal price) => price;
    }

    public sealed class VipDiscount : IDiscountStrategy
    {
        public string Name => "VIP-скидка (10%)";

        public decimal Apply(decimal price) => price * 0.9m;
    }

    public sealed class PromoDiscount : IDiscountStrategy
    {
        public string Name => "Промо-скидка (20%)";

        public decimal Apply(decimal price) => price * 0.8m;
    }

    public sealed class PriceCalculator
    {
        private readonly IDiscountStrategy _strategy;

        public PriceCalculator(IDiscountStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
        public decimal Calculate(decimal price) => _strategy.Apply(price);
    }
    

}
