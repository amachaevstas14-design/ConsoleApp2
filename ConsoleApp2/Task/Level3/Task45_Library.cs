using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level3
{
    public sealed class Task45_Library : ITask
    {
        public string Title { get; } = "Библиотека: выдача и возврат книг";

        public string Description { get; } =
            "Библиотека хранит каталог книг. Читатель может одновременно держать не более " +
            "трёх книг. Если книга уже выдана, BorrowBook бросает BookAlreadyIssuedException; " +
            "если у читателя уже три книги, — TooManyBooksException. Возврат книги, которой " +
            "читатель не владеет, бросает BookNotIssuedException. После возврата книга снова " +
            "становится доступной. Все сценарии показаны в демонстрации.";

        public string[] Requirements { get; } = new[]
        {
            "Book: Id, Title и изменяемый флаг IsAvailable.",
        "Reader: Name и список IDs выданных книг.",
        "Library.BorrowBook: BookAlreadyIssuedException, если книга уже выдана; TooManyBooksException, если у читателя уже 3 книги.",
        "Library.ReturnBook: BookNotIssuedException, если читатель не брал эту книгу; после возврата книга снова доступна.",
        "Демонстрация: успешные операции, все три ошибки и повторная выдача после возврата."
        };

        private static void Attempt(string action, Action operation, string successMessage)
        {
            Console.WriteLine(action);
            try
            {
                operation();
                Console.WriteLine($"  Результат: {successMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Поймано {ex.GetType().Name}: {ex.Message}");
            }

            Console.WriteLine();
        }

        public void Run()
        {
            Console.WriteLine(Title);

            var library = new Library(new[]
            {
            new Book(1, "Мастер и Маргарита"),
            new Book(2, "Идиот"),
            new Book(3, "Преступление и наказание"),
            new Book(4, "Война и мир"),
            new Book(5, "Три товарища")
        });
            var anna = new Reader("Анна");
            var boris = new Reader("Борис");

            Console.WriteLine($"Каталог: {library.Count} книг, лимит: {Library.MaxBooksPerReader} на читателя");
            Console.WriteLine($"Читатели: {anna} и {boris}");
            Console.WriteLine();

            Attempt("1) Анна берёт «Мастер и Маргарита»", () => library.BorrowBook(anna, library.FindBook(1)), "книга выдана");
            Attempt("2) Анна берёт «Идиот»", () => library.BorrowBook(anna, library.FindBook(2)), "книга выдана");
            Attempt("3) Борис берёт «Мастер и Маргариту» (уже у Анны)", () => library.BorrowBook(boris, library.FindBook(1)), "книга выдана");
            Attempt("4) Анна берёт «Преступление и наказание» (у неё уже 3 книги? нет, будет третья)", () => library.BorrowBook(anna, library.FindBook(3)), "книга выдана (у Анны стало 3)");
            Attempt("5) Анна берёт «Войну и мир» (лимит 3 превышен)", () => library.BorrowBook(anna, library.FindBook(4)), "книга выдана");
            Attempt("6) Борис берёт «Войну и мир»", () => library.BorrowBook(boris, library.FindBook(4)), "книга выдана");
            Attempt("7) Борис берёт «Три товарища»", () => library.BorrowBook(boris, library.FindBook(5)), "книга выдана");
            Attempt("8) Борис возвращает «Мастера и Маргариту» (он её не брал)", () => library.ReturnBook(boris, library.FindBook(1)), "книга возвращена");
            Attempt("9) Анна возвращает «Мастера и Маргариту»", () => library.ReturnBook(anna, library.FindBook(1)), "книга возвращена");
            Attempt("10) Борис снова берёт «Мастера и Маргариту» (после возврата)", () => library.BorrowBook(boris, library.FindBook(1)), "книга выдана повторно");

            Console.WriteLine("Итоговое состояние:");
            foreach (var book in library.Catalog)
            {
                Console.WriteLine($"  [{book.Id}] «{book.Title}»: {(book.IsAvailable ? "в каталоге" : "выдана")}");
            }

            foreach (var reader in new[] { anna, boris })
            {
                var titles = reader.BorrowedBookIds
               .Select(id => library.FindBook(id))
               .Where(b => b != null)
               .Select(b => b.Title)
               .ToList();
                Console.WriteLine($"  {reader}: {(titles.Count == 0 ? "книг нет" : string.Join(", ", titles))}");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Пояснение: у книги есть флаг IsAvailable, который меняется при выдаче и возврате, " +
                "поэтому одна физическая книга может быть у читателя только одна. " +
                "Лимит в 3 книги хранится у читателя (список выданных IDs), а ошибки оформлены " +
                "собственными исключениями — вызывающий код может обрабатывать каждую ситуацию " +
                "отдельно (книга занята / превышен лимит / возврат не выданной книги).");
        }
    }
    public sealed class Book
    {
        public int Id { get; }
        public string Title { get; }
        public bool IsAvailable { get; private set; }

        public Book(int id, string title)
        {
            Id = id;
            Title = title;
            IsAvailable = true;
        }
        public void Issue()
        {
            IsAvailable = false;
        }
        public void Release()
        {
            IsAvailable = true;
        }
    }
    public sealed class Reader
    {
        public string Name { get; }

        public List<int> BorrowedBookIds { get; } = new List<int>();

        public Reader(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }

    public sealed class Library
    {
        public const int MaxBooksPerReader = 3;

        private readonly List<Book> _catalog = new List<Book>();
        private readonly Dictionary<int, Book> _booksById = new Dictionary<int, Book>();

        public Library(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                AddBook(book);
            }
        }

        public IReadOnlyList<Book> Catalog => _catalog;
        public int Count => _catalog.Count;

        public void AddBook(Book book)
        {
            if (_booksById.ContainsKey(book.Id))
            {
                throw new InvalidOperationException($"Книга с Id {book.Id} уже есть в каталоге.");
            }

            _catalog.Add(book);
            _booksById[book.Id] = book;
        }

        public Book FindBook(int id)
        {
            return _booksById.TryGetValue(id, out var book) ? book : null;
        }

        public void BorrowBook(Reader reader, Book book)
        {
            if (!book.IsAvailable)
            {
                throw new BookAlreadyIssuedException(reader.Name, book);
            }

            if (reader.BorrowedBookIds.Count >= MaxBooksPerReader)
            {
                throw new TooManyBooksException(reader.Name, MaxBooksPerReader);
            }

            reader.BorrowedBookIds.Add(book.Id);
            book.Issue();
        }

        public void ReturnBook(Reader reader, Book book)
        {
            if (!reader.BorrowedBookIds.Contains(book.Id))
            {
                throw new BookNotIssuedException(reader.Name, book);
            }

            reader.BorrowedBookIds.Remove(book.Id);
            book.Release();
        }
    }
    public sealed class BookAlreadyIssuedException : Exception
    {
        public BookAlreadyIssuedException(string readerName, Book book)
            : base($"Книга «{book.Title}» уже выдана, её не может взять {readerName}.")
        {
        }
    }

    public sealed class TooManyBooksException : Exception
    {
        public TooManyBooksException(string readerName, int limit)
            : base($"Читатель {readerName} уже держит {limit} книг — больше выдавать нельзя.")
        {
            Limit = limit;
        }
        public int Limit { get; }
    }

    public sealed class BookNotIssuedException : Exception
    {
        public BookNotIssuedException(string readerName, Book book)
            : base($"Читатель {readerName} не брал книгу «{book.Title}» — возвращать нечего.")
        {
        }
    }
}
