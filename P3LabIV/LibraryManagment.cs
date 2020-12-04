using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace P3LabIV
{
    class LibraryManagment
    {


        private const string BOOKS_FILE_NAME = "../../../books.txt";
        private const string LIBRARY_CONFIG = "../../../library_config.txt";
        string BOOKS_PATH = Path.Combine(Environment.CurrentDirectory, BOOKS_FILE_NAME);
        string LIBRARY_CONFIG_PATH = Path.Combine(Environment.CurrentDirectory, LIBRARY_CONFIG);

        private const int TITLE_NUMBER = 0;
        private const int AUTHOR_NUMBER = 1;
        private const int RACK_NUMBER = 2;
        private const int SHELF_NUMBER = 3;
        private const int SPOT_NUMBER = 4;

        public static int RACK_QUANTITY;
        public static int SHELF_QUANTITY;
        public static int SPOT_QUANTITY;
        Book[,,] books;


        public void ConfigureLibrary()
        {
            string[] libraryInfo;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(LIBRARY_CONFIG_PATH);
            if ((line = file.ReadLine()) != null)
            {
                libraryInfo = line.Split(";").ToArray();
                if (libraryInfo == null || libraryInfo.Length < 5)
                    throw new ArgumentException("Rozmiar biblioteki w pliku library_config nie jest zdefiniowany");
                RACK_QUANTITY = int.Parse(libraryInfo[RACK_NUMBER]);
                SHELF_QUANTITY = int.Parse(libraryInfo[SHELF_NUMBER]);
                SPOT_QUANTITY = int.Parse(libraryInfo[SPOT_NUMBER]);
                books = new Book[RACK_QUANTITY, SHELF_QUANTITY, SPOT_QUANTITY];
            }

            file.Close();
        }

        public void LoadBooks()
        {
            string[] bookInfo;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(BOOKS_PATH);

            while ((line = file.ReadLine()) != null)
            {
                bookInfo = line.Split(";").ToArray();
                int Rack = int.Parse(bookInfo[RACK_NUMBER]);
                int Shelf = int.Parse(bookInfo[SHELF_NUMBER]);
                int Spot = int.Parse(bookInfo[SPOT_NUMBER]);
                books[Rack, Shelf, Spot] =
                    new Book(bookInfo[TITLE_NUMBER], bookInfo[AUTHOR_NUMBER], Rack, Shelf, Spot);
            }
            file.Close();
        }

        public Book FindBook(String sentence)
        {
            for (int i = 0; i < RACK_QUANTITY; i++)
            {
                for (int j = 0; j < SHELF_QUANTITY; j++)
                {
                    for (int k = 0; k < SPOT_QUANTITY; k++)
                    {
                        if (books[i, j, k].Title.Contains(sentence) || books[i, j, k].Author.Contains(sentence))
                            return books[i, j, k];
                    }
                }
            }

            return new Book();
        }

        public Book TakeBook(int rack, int shelf, int spot)
        {
            return books[rack, shelf, spot];
        }

        public void PutBook(Book book, int rack, int shelf, int spot)
        {
            books[rack, shelf, spot] = book;
        }

        public void SaveBooks()
        {

            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(BOOKS_PATH, false))
            {
                for (int i = 0; i < RACK_QUANTITY; i++)
                {
                    for (int j = 0; j < SHELF_QUANTITY; j++)
                    {
                        for (int k = 0; k < SPOT_QUANTITY; k++)
                        {

                            file.WriteLine(books[i, j, k].TextFileExport());

                        }
                    }
                }
            }
        }



        override public string ToString()
        {
            string library_books;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < RACK_QUANTITY; i++)
            {
                builder.Append("Regał " + (i + 1) + "\n");
                for (int j = 0; j < SHELF_QUANTITY; j++)
                {
                    builder.Append("Półka " + (j + 1) + "\n");
                    for (int k = 0; k < SPOT_QUANTITY; k++)
                    {
                        builder.Append("| Miejsce " + (k + 1) + " | ");
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
