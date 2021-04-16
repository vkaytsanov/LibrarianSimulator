using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Book
{
    public class BookCharacteristics
    {
        private String title;
        private String author;
        private Color color;

        public BookCharacteristics(string title, string author)
        {
            this.title = title;
            this.author = author;
        }

        public override string ToString()
        {
            return "Title: " + title + ", Author: " + author;
        }
    }
}