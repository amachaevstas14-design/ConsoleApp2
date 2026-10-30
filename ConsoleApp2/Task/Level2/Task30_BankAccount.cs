using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task30_BankAccount: ITask
    {
        public string Title { get; } = "BankAccount + свой Exception";

        public string Description { get; } =
            "Реализовать банковский счёт с собственным исключением InsufficientFundsException.";

        public string[] Requirements { get; } = new[]
        {
            "Класс BankAccount с приватным балансом.",
        "Методы Deposit(decimal) и Withdraw(decimal).",
        "Свой класс InsufficientFundsException : Exception.",
        "Withdraw бросает исключение, если сумма больше баланса.",
        "Продемонстрировать успешное снятие и попытку снять больше баланса (с try/catch)."
        };

        public void Run()
        {
            var account = new BankAccount(1000m);

            Console.WriteLine($"Начальный баланс: {account.Balance}");

            account.Deposit(500m);
            Console.WriteLine($"После пополнения на 500: {account.Balance}");

            account.Withdraw(400m);
            Console.WriteLine($"После снятия 400: {account.Balance}");

            Console.WriteLine();
            Console.WriteLine("Попытка снять 5000 (больше баланса):");

            try
            {
                account.Withdraw(5000m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Свойство Balance: {ex.Balance}");
                Console.WriteLine($"Свойство Requested: {ex.Requested}");
            }

            Console.WriteLine();
            Console.WriteLine($"Баланс после неудачного снятия: {account.Balance}");
            Console.WriteLine("Баланс не ушёл в минус.");
        }
    }

    public sealed class BankAccount
    {
        private decimal _balance;

        public BankAccount(decimal initialBalance = 0)
        {
            _balance = initialBalance;
        }

        public decimal Balance => _balance;

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Сумма пополнения не может быть отрицательной.");
            }

            _balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Сумма к снятию не может быть отрицательной.");
            }

            if (amount > _balance)
            {
                throw new InsufficientFundsException(_balance, amount);
            }

            _balance -= amount;
        }
    }

    public sealed class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(decimal balance, decimal requested)
            : base($"Недостаточно средств: на счёте {balance}, запрошено {requested}.")
        {
            Balance = balance;
            Requested = requested;
        }

        public decimal Balance { get; }

        public decimal Requested { get; }
    }
}

