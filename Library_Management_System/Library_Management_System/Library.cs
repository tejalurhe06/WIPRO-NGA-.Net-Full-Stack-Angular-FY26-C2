using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Library
    {
        //properties
        public List<Book> Books { get; set; }
        public List<Borrower> Borrowers { get; set; }

        //default constructor
        public Library()
        { 
            Books = new List<Book>();
            Borrowers = new List<Borrower>();
        }

        //parameterized constructor
        public Library(List<Book> books,List<Borrower> borrowers) 
        {
            Books = books;
            Borrowers = borrowers;
        }

        // When the user send the book info from main in book object then we
        // add here in the list of books present in the library
        public void AddBook(Book book)
        {
            Books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        //Register Borrower Method
        public void RegisterBorrower(Borrower borrower) 
        {
            Borrowers.Add(borrower);
            Console.WriteLine($"Borrower '{borrower.Name}' registered.");
        }

        //Borrow Book using the isbn and librarycard number
        public void BorrowBook(string isbn,string librarycardnumber)
        {
            Book book = Books.Find(b => b.ISBN == isbn);
            int cardNo = int.TryParse(librarycardnumber, out int result) ? result : -1;
            Borrower borrower = Borrowers.Find(b => b.LibraryCardNumber == cardNo);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return;
            }

            borrower.BorrowBook(book);

        }

        //ReturnBook Method
        public void ReturnBook(string isbn, string librarycardnumber) 
        {

            Book book = Books.Find(b => b.ISBN == isbn);
            int cardNo = int.TryParse(librarycardnumber, out int result) ? result : -1;
            Borrower borrower = Borrowers.Find(b => b.LibraryCardNumber == cardNo);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return;
            }

            borrower.ReturnBook(book);
        }

        //View the books in the book list that are available and borrowed
        public void ViewBooks()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("Books in Library:");
            foreach (var book in Books)
            {
                string status = book.IsBorrowed ? "Borrowed" : "Available";
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Status: {status}");
            }

        }

        //ViewBorrowers method
        public void ViewBorrowers()
        {
            if (Borrowers.Count == 0)
            {
                Console.WriteLine("No borrowers registered.");
                return;
            }

            Console.WriteLine("Registered Borrowers:");
            foreach (var borrower in Borrowers)
            {
                Console.WriteLine($"Name: {borrower.Name}, Card No: {borrower.LibraryCardNumber}, Books Borrowed: {borrower.BorrowedBooks.Count}");
            }
        }

    }
}
