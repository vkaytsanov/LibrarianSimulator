using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Book
{
    public static class BooksDB
    {
        private static Random random = new Random();

        private static List<BookCharacteristics> _fictions = new List<BookCharacteristics>();


        static BooksDB()
        {
            using (StreamReader sr = File.OpenText("Assets/Database/fictions.txt"))
            {
                string bookTitle;
                while ((bookTitle = sr.ReadLine()) != null)
                {
                    string bookAuthor = sr.ReadLine();

                    _fictions.Add(new BookCharacteristics(bookTitle, bookAuthor));
                    
                }

                _fictions.Sort();
            }

        }

        public static BookCharacteristics GetRandomFictionBookCharacteristics()
        {
            return _fictions[random.Next(_fictions.Count)];
        }

        public static bool SearchForBook(string query)
        {
            
            return _fictions.BinarySearch(new BookCharacteristics(query, null)) < 0;
        }
    }
}