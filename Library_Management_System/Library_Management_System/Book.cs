using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Book
    {
        //properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }

        //default constructor
        public Book()
        {
            Title = "No title";
            Author = "No author";
            ISBN = "No isbn";
            IsBorrowed = false;
        }

        //parameterized constructor
        public Book(string title, string author, string isbn, bool isBorrowed)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsBorrowed = isBorrowed;
        }

        //Borrow book method
        public void Borrow()
        {
            if (!IsBorrowed)
            {
                IsBorrowed = true;
                Console.WriteLine($"The book '{Title}' has been borrowed.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' is already borrowed.");
            }
        }

        //Return book method
        public void Return()
        {
            if (IsBorrowed)
            {
                IsBorrowed = false;
                Console.WriteLine($"The book '{Title}' has been returned.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' was not borrowed.");
            }

        }
    }
}
