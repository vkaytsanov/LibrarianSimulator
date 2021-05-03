using System;
using System.Collections.Generic;
using System.IO;
using NPC;
using Random = UnityEngine.Random;

namespace Dialog {

	public static class DialogDB {
		private static readonly List<string> ReturningBooks = new List<string>();
		private static readonly List<string> WantingBooks = new List<string>();
		private static readonly List<string> Registration = new List<string>();

		static DialogDB() {
			ParseSentences(ReturningBooks, "returning_books");
			ParseSentences(WantingBooks, "wanting_books");
			ParseSentences(Registration, "registration");
		}

		private static void ParseSentences(List<string> db, string fileName) {
			StreamReader sr = File.OpenText("Assets/Database/" + fileName + ".txt");
			string sentence;
			while ((sentence = sr.ReadLine()) != null) {
				db.Add(sentence);
			}

			sr.Close();
		}

		public static string GetRandomSentence(NPCAction action, string actionInfo) {
			string formattedSentence;
			switch (action) {
				case NPCAction.ReturningBook:
					formattedSentence = ReturningBooks[Random.Range(0, ReturningBooks.Count)];
					break;
				case NPCAction.WantingBook:
					formattedSentence = WantingBooks[Random.Range(0, WantingBooks.Count)];
					break;
				case NPCAction.Registration:
					formattedSentence = Registration[Random.Range(0, Registration.Count)];
					break;
				default:
					throw new NotImplementedException();
			}

			if (formattedSentence.HasPlaceholder()) {
				formattedSentence = String.Format(formattedSentence, actionInfo);
			}

			return formattedSentence;
		}
	}

}