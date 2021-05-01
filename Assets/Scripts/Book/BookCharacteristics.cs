using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Book
{
    public class BookCharacteristics : IComparable<BookCharacteristics>
    {
        public readonly string Title;
        public readonly string Author;

        public BookCharacteristics(string title, string author)
        {
            this.Title = title;
            this.Author = author;
        }

        public override string ToString()
        {
            return "Title: " + Title + ", Author: " + Author;
        }
        
        protected bool Equals(BookCharacteristics other)
        {
            return Title == other.Title;
        }
        
        protected bool Equals(string other)
        {
            return Title == other;
        }

        public int CompareTo(BookCharacteristics other)
        {
            if (other == null) return 1;
            return Title.CompareTo(other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return CompareTo((BookCharacteristics) obj) < 0;
        }

        public override int GetHashCode()
        {
            return (Title != null ? Title.GetHashCode() : 0);
        }

        private sealed class TitleEqualityComparer : IComparer<BookCharacteristics>
        {
            public int Compare(BookCharacteristics x, BookCharacteristics y)
            {
                return String.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static IComparer<BookCharacteristics> TitleComparer { get; } = new TitleEqualityComparer();
    }
}