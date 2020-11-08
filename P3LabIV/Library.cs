using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace P3LabIV
{
    public class Library
    {
        private const string FILE_NAME = "../../../books.txt";
        string PATH = Path.Combine(Environment.CurrentDirectory, FILE_NAME);
        private const int TITLE_NUMBER = 0;
        private const int AUTHOR_NUMBER = 1;
        private const int RACK_NUMBER = 2;
        private const int SHELF_NUMBER = 3;
        private const int SPOT_NUMBER = 4;

        Book[,,] books;
        DataReader reader;

        public Library()
        {
            books = new Book[3, 6, 10];
            reader = new DataReader();
            LoadBooks();
            Menu();
        }

        private void LoadBooks()
        {
            string[] bookInfo;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(PATH);
            while ((line = file.ReadLine()) != null)
            {
                bookInfo = line.Split(";").ToArray();
                int Rack = int.Parse(bookInfo[RACK_NUMBER]);
                int Shelf = int.Parse(bookInfo[SHELF_NUMBER]);
                int Spot = int.Parse(bookInfo[SPOT_NUMBER]);
                books[Rack,Shelf,Spot] =
                    new Book(bookInfo[TITLE_NUMBER], bookInfo[AUTHOR_NUMBER], Rack, Shelf, Spot);
            }
            file.Close();
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
                        PrintBook(SearchBook()); break;
                    case 2:
                        ManageBook(); break;
                    case 3:
                        PrintBooks(); break;
                    default:
                        Console.WriteLine("Błędna opcja w menu"); break;
                }
            } while (option!=0);
        }
        
      public Book SearchBook()
        {
            string sentence = reader.readSentence();
            Book book = new Book();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (books[i, j, k].Title.Contains(sentence) || books[i, j, k].Author.Contains(sentence))
                            return books[i, j, k];
                    }
                }
            }
            return book;
        }
      public void ManageBook()
        {
            Console.WriteLine("Podaj numer szafy(1-3)");
            int rack = reader.readIntValue(3)-1;
            Console.WriteLine("Podaj numer półki(1-6)");
            int shelf = reader.readIntValue(6)-1;
            Console.WriteLine("Podaj numer miejsca(1-10)");
            int spot = reader.readIntValue(10)-1;
            Book bookFromLib = books[rack, shelf, spot];
            PrintBook(bookFromLib);
            Console.WriteLine("Podaj nowy tytuł");
            bookFromLib.Title = reader.readSentence();
            Console.WriteLine("Podaj nowego autora");
            bookFromLib.Author = reader.readSentence();
            books[rack, shelf, spot] = bookFromLib;
            SaveBooks();
        }

      public void SaveBooks()
        {
            File.WriteAllText(PATH, string.Empty);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        using (System.IO.StreamWriter file =
                             new System.IO.StreamWriter(PATH, true))
                         {
                             file.WriteLine(books[i,j,k].TextFileExport());
                         }
                    }
                }
            }
        }
      public void PrintBooks()
        {
            Console.WriteLine(ToString());
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
      override public string ToString()
        {
            string library_books;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                builder.Append("Regał " + (i+1)+"\n");
                for (int j = 0; j < 6; j++)
                {
                    builder.Append("Półka "+ (j + 1) + "\n");
                    for (int k = 0; k < 10; k++)
                    {
                        builder.Append("| Miejsce "+(k+1) + " | ");
                        builder.Append(books[i, j, k].ToString());
                        builder.Append("\n");
                    }
                    builder.Append("\n");
                }
                builder.Append("\n\n");
            }
            library_books = builder.ToString();
            return library_books;
        }
    }
}