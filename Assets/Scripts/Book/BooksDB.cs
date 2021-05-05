using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Random = UnityEngine.Random;

namespace Book {

	public static class BooksDB {
		private static readonly List<BookData> Fictions = new List<BookData>();

		static BooksDB() {
			using (StreamReader sr = File.OpenText("Assets/Database/fictions.txt")) {
				string bookTitle;
				while ((bookTitle = sr.ReadLine()) != null) {
					string bookAuthor = sr.ReadLine();

					Fictions.Add(new BookData(bookTitle, bookAuthor));
				}

				Fictions.Sort();
			}
		}

		public static BookData GetRandomFictionBookCharacteristics() {
			return Fictions[Random.Range(0, Fictions.Count)];
		}
	}

}