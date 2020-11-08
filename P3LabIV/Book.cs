using System;

namespace P3LabIV
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int RackNumber { get; set; }
        public int ShelfNumber { get; set; }
        public int SpotNumber { get; set; }

        public Book()
        {

        }

        public Book(string title, string author, int rackNumber, int shelfNumber, int spotNumber)
        {
            Title = title;
            Author = author;
            RackNumber = rackNumber;
            ShelfNumber = shelfNumber;
            SpotNumber = spotNumber;
        }

        public string ToString()
        {
            return "[ Tytuł: " + Title + " | Autor: " + Author + " ]";
        }

        public string BookLocation()
        {
            return "[ Regał: " + (RackNumber+1) + " | Półka: " + (ShelfNumber + 1) + " | Miejsce: " + (SpotNumber + 1) + " ]";

        }

        public string TextFileExport()
        {
            return Title + ";" + Author + ";" + RackNumber + ";" + ShelfNumber + ";" + SpotNumber;
        }

        
    }
}