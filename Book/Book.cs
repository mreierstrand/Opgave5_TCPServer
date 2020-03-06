using System;

namespace Book
{
    public class Book
    {
        private string _title;
        private string _author;
        private int _noOfPages;
        private string _isbn13;

        public string Title
        {
            get { return _title; }
            set
            {
                //
                if (value == null) throw new NullReferenceException();
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (value == null) throw new NullReferenceException();
                if (value.Length < 2) throw new ArgumentOutOfRangeException();
                _author = value;
            }
        }

        public int NoOfPages
        {
            get { return _noOfPages; }
            set
            {

                if (value <= 4 || value >= 1000) throw new ArgumentOutOfRangeException();
                _noOfPages = value;
            }
        }

        public string ISBN13
        {
            get { return _isbn13; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (value.Length == 13) _isbn13 = value;
                else if (value.Length != 13) throw new ArgumentOutOfRangeException();
            }
        }

        public Book(string title, string author, int noOfPages, string isbn13)
        {
            Title = title;
            Author = author;
            NoOfPages = noOfPages;
            ISBN13 = isbn13;
        }

        public Book()
        {

        }
    }
}
