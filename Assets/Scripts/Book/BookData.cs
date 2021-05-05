using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Book {

	public enum BookReturnState {
		Expired,
		Ok
	}

	public class BookData : IComparable<BookData> {
		public readonly string Title;
		public readonly string Author;
		public Sprite Sprite;
		public BookReturnState BookReturnState;

		public BookData(string title, string author) {
			Title = title;
			Author = author;
		}

		public override string ToString() {
			return "Title: " + Title + ", Author: " + Author;
		}

		protected bool Equals(BookData other) {
			return Title == other.Title;
		}

		protected bool Equals(string other) {
			return Title == other;
		}

		public int CompareTo(BookData other) {
			if (other == null) return 1;
			return Title.CompareTo(other.Title);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return CompareTo((BookData) obj) < 0;
		}

		public override int GetHashCode() {
			return (Title != null ? Title.GetHashCode() : 0);
		}

		private sealed class TitleEqualityComparer : IComparer<BookData> {
			public int Compare(BookData x, BookData y) {
				return String.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase);
			}
		}

		public static IComparer<BookData> TitleComparer { get; } = new TitleEqualityComparer();
	}

}