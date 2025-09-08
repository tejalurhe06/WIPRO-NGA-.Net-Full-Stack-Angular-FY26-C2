using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Borrower
    {
        //properties
        public string Name { get; set; }
        public int LibraryCardNumber { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        //default constructor
        public Borrower() 
        {
            Name = "No name";
            LibraryCardNumber = 0;
            BorrowedBooks = new List<Book>();
        }

        //parameterized constructor
        public Borrower(string name,int libcardno, List<Book> borrowedbooks) 
        {
            Name = name;
            LibraryCardNumber = libcardno;
            BorrowedBooks = borrowedbooks;
        }

        //BorrowBook method
        public void BorrowBook(Book book)
        {
            if (!book.IsBorrowed)
            {
                book.IsBorrowed = true;
                BorrowedBooks.Add(book);
                Console.WriteLine($"{Name} borrowed the book '{book.Title}'.");
            }
            else
            {
                Console.WriteLine($"The book '{book.Title}' is already borrowed.");
            }

        }

        //ReturnBook method
        public void ReturnBook(Book book)
        {
            if (BorrowedBooks.Contains(book))
            {
                book.IsBorrowed = false;
                BorrowedBooks.Remove(book);
                Console.WriteLine($"{Name} returned the book '{book.Title}'.");
            }
            else
            {
                Console.WriteLine($"{Name} cannot return '{book.Title}' as it was not borrowed by them.");
            }

        }
    }
}
