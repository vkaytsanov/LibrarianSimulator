using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Book
{
    public class BookCharacteristics : IComparable<BookCharacteristics>
    {
        public string title;
        public string author;

        public BookCharacteristics(string title, string author)
        {
            this.title = title;
            this.author = author;
        }

        public override string ToString()
        {
            return "Title: " + title + ", Author: " + author;
        }
        
        protected bool Equals(BookCharacteristics other)
        {
            return title == other.title;
        }
        
        protected bool Equals(string other)
        {
            return title == other;
        }

        public int CompareTo(BookCharacteristics other)
        {
            if (other == null) return 1;
            return title.CompareTo(other.title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((string) obj);
        }

        public override int GetHashCode()
        {
            return (title != null ? title.GetHashCode() : 0);
        }
    }
}