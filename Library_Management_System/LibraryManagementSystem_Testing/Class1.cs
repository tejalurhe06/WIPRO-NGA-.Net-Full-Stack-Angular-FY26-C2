using Library_Management_System;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem_Testing
{
    [TestFixture]
    public class LibraryTests
    {
        private Library library;

        [SetUp]
        public void Setup()
        {
            library = new Library();
        }

        // 1. Test Adding a Book
        [Test]
        public void AddBook_Should_AddBookToLibrary()
        {
            var book = new Book("Test Title", "Test Author", "123", false);
            library.AddBook(book);

            Assert.Contains(book, library.Books);
            Assert.AreEqual(1, library.Books.Count);
        }

        // 2. Test Registering a Borrower
        [Test]
        public void RegisterBorrower_Should_AddBorrowerToLibrary()
        {
            var borrower = new Borrower("Tejal", 1001, new List<Book>());
            library.RegisterBorrower(borrower);

            Assert.Contains(borrower, library.Borrowers);
            Assert.AreEqual(1, library.Borrowers.Count);
        }

        // 3. Borrowing a Book - Book should be marked as borrowed
        [Test]
        public void BorrowBook_Should_MarkBookAsBorrowed()
        {
            var book = new Book("C# Book", "John Doe", "ABC123", false);
            var borrower = new Borrower("Tejal", 1001, new List<Book>());
            library.AddBook(book);
            library.RegisterBorrower(borrower);

            library.BorrowBook("ABC123", "1001");

            Assert.IsTrue(book.IsBorrowed);
        }

        // 3. Borrowing a Book - Book should be in borrower's list
        [Test]
        public void BorrowBook_Should_AddBookToBorrowerList()
        {
            var book = new Book("C# Book", "John Doe", "ABC123", false);
            var borrower = new Borrower("Tejal", 1001, new List<Book>());
            library.AddBook(book);
            library.RegisterBorrower(borrower);

            library.BorrowBook("ABC123", "1001");

            Assert.Contains(book, borrower.BorrowedBooks);
        }

        // 4. Returning a Book - Should be marked available
        [Test]
        public void ReturnBook_Should_MarkBookAsAvailable()
        {
            var book = new Book("C# Book", "John Doe", "ABC123", false);
            var borrower = new Borrower("Tejal", 1001, new List<Book>());
            library.AddBook(book);
            library.RegisterBorrower(borrower);
            library.BorrowBook("ABC123", "1001");

            library.ReturnBook("ABC123", "1001");

            Assert.IsFalse(book.IsBorrowed);
        }

        // 4. Returning a Book - Should be removed from borrower's list
        [Test]
        public void ReturnBook_Should_RemoveBookFromBorrowerList()
        {
            var book = new Book("C# Book", "John Doe", "ABC123", false);
            var borrower = new Borrower("Tejal", 1001, new List<Book>());
            library.AddBook(book);
            library.RegisterBorrower(borrower);
            library.BorrowBook("ABC123", "1001");

            library.ReturnBook("ABC123", "1001");

            Assert.IsFalse(borrower.BorrowedBooks.Contains(book));
        }

        // 5. ViewBooks - Ensure book count is correct
        [Test]
        public void ViewBooks_Should_ShowCorrectBookCount()
        {
            library.AddBook(new Book("A", "B", "123", false));
            library.AddBook(new Book("C", "D", "456", false));

            Assert.AreEqual(2, library.Books.Count);
        }

        // 5. ViewBorrowers - Ensure borrower count is correct
        [Test]
        public void ViewBorrowers_Should_ShowCorrectBorrowerCount()
        {
            library.RegisterBorrower(new Borrower("Alice", 1, new List<Book>()));
            library.RegisterBorrower(new Borrower("Bob", 2, new List<Book>()));

            Assert.AreEqual(2, library.Borrowers.Count);
        }
    }
}
