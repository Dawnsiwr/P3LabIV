using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace P3LabIV
{
    public class Library
    {   
        DataReader reader;
        LibraryManagment libraryManagment;
        public Library()
        {
            libraryManagment = new LibraryManagment();
            reader = new DataReader();
        }

       public void Run()
        {
            libraryManagment.ConfigureLibrary();
            libraryManagment.LoadBooks();
            Menu();
        }

        public void Menu()
        {
            
            int option = 0;
            do
            {
                Console.WriteLine("Witaj w bibliotece");
                Console.WriteLine("1. Wyszukaj książkę");
                Console.WriteLine("2. Modyfikuj książkę");
                Console.WriteLine("3. Sprawdź wszystkie książki");
                Console.WriteLine("0. Wyjdź");

                option = reader.readIntValue();
                switch (option)
                {
                    case 0:
                        Environment.Exit(0); break;
                    case 1:
                        PrintBook(FindBook()); break;
                    case 2:
                        ManageBook(); break;
                    case 3:
                        PrintBooks(); break;
                    default:
                        Console.WriteLine("Błędna opcja w menu"); break;
                }
            } while (option!=0);
        }
        
      public Book FindBook()
        {
            Console.Write("Wprowadź fragment do wyszukania: ");
            string sentence = reader.readSentence();
            Book book = libraryManagment.FindBook(sentence);
            if(book.Author!=null && book.Title!=null)
                Console.WriteLine("Znaleziono książkę");
         
            return book;
        }
      public void ManageBook()
        {
            Console.WriteLine("Podaj numer szafy(1-"+LibraryManagment.RACK_QUANTITY+")");
            int rack = reader.readIntValue(LibraryManagment.RACK_QUANTITY) -1;
            Console.WriteLine("Podaj numer półki(1-"+LibraryManagment.SHELF_QUANTITY+")");
            int shelf = reader.readIntValue(LibraryManagment.SHELF_QUANTITY) -1;
            Console.WriteLine("Podaj numer miejsca(1-"+LibraryManagment.SPOT_QUANTITY + ")");
            int spot = reader.readIntValue(LibraryManagment.SPOT_QUANTITY) -1;
            Book bookFromLib = libraryManagment.TakeBook(rack, shelf, spot);
            PrintBook(bookFromLib);
            Console.WriteLine("Podaj nowy tytuł");
            bookFromLib.Title = reader.readSentence();
            Console.WriteLine("Podaj nowego autora");
            bookFromLib.Author = reader.readSentence();
            libraryManagment.PutBook(bookFromLib, rack,shelf,spot);
            libraryManagment.SaveBooks();
        }

      
      public void PrintBooks()
        {
            Console.WriteLine(libraryManagment.ToString());
        }
      public void PrintBook(Book book)
        {
            if (book.Title != null && book.Author != null)
            {
                Console.WriteLine(book.ToString());
                Console.WriteLine(book.BookLocation());
            }
            else
                Console.WriteLine("Nie znaleziono książki");
        }
     
    }
}