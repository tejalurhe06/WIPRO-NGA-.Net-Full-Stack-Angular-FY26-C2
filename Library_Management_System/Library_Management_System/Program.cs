using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Program
    {
        //entry point
        static void Main(string[] args)
        {
            //Library class object 
            Library library = new Library();
            int choice;

            do
            {
                Console.WriteLine("\n--- Library Management Menu ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Register Borrower");
                Console.WriteLine("4. View Borrowers");
                Console.WriteLine("5. Borrow Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                // take input from user and convert it into integer if conversion is successfull then stores in choice 
                // and bool valid is true if the input is a valid integer otherwise false
                bool valid = int.TryParse(Console.ReadLine(), out choice);

                if (!valid)
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Book Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Book Author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter Book ISBN: ");
                        string isbn = Console.ReadLine();

                        Book book = new Book(title, author, isbn, false);
                        library.AddBook(book);
                        break;

                    case 2:
                        library.ViewBooks();
                        break;

                    case 3:
                        Console.Write("Enter Borrower Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Library Card Number: ");
                        int cardNo = Convert.ToInt32(Console.ReadLine());

                        Borrower borrower = new Borrower(name, cardNo, new List<Book>());
                        library.RegisterBorrower(borrower);
                        break;

                    case 4:
                        library.ViewBorrowers();
                        break;

                    case 5:
                        Console.Write("Enter Book ISBN to Borrow: ");
                        string borrowIsbn = Console.ReadLine();
                        Console.Write("Enter Library Card Number: ");
                        string borrowerCard = Console.ReadLine();
                        library.BorrowBook(borrowIsbn, borrowerCard);
                        break;

                    case 6:
                        Console.Write("Enter Book ISBN to Return: ");
                        string returnIsbn = Console.ReadLine();
                        Console.Write("Enter Library Card Number: ");
                        string returnCard = Console.ReadLine();
                        library.ReturnBook(returnIsbn, returnCard);
                        break;

                    case 0:
                        Console.WriteLine("Exiting program...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

            } while (true);

        }
    }
}
