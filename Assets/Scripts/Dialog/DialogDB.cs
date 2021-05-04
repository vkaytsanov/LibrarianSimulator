using System;
using System.Collections.Generic;
using System.IO;
using NPC;
using Random = UnityEngine.Random;

namespace Dialog {

	public enum NpcDialogueType : sbyte{
		ReturningBook = 0,
		WantingBook = 1,
		Registration = 2
	}
	
	public static class DialogDB {
		private const int TypesCount = 3;
		private static readonly List<string>[] Dialogues = new List<string>[TypesCount];

		static DialogDB() {
			for (int i = 0; i < TypesCount; i++) {
				Dialogues[i] = new List<string>();
			}
			ParseSentences(Dialogues[(int) NpcDialogueType.ReturningBook], "returning_books");
			ParseSentences(Dialogues[(int) NpcDialogueType.WantingBook], "wanting_books");
			ParseSentences(Dialogues[(int) NpcDialogueType.Registration], "registration");
		}

		private static void ParseSentences(List<string> db, string fileName) {
			StreamReader sr = File.OpenText("Assets/Database/" + fileName + ".txt");
			string sentence;
			while ((sentence = sr.ReadLine()) != null) {
				db.Add(sentence);
			}

			sr.Close();
		}

		public static string GetRandomSentence(NpcDialogueType type, string additionalInfo) {
			
			int idx = (int) type;
			string sentence = Dialogues[idx][Random.Range(0, Dialogues[idx].Count)];

			if (sentence.HasPlaceholder()) {
				sentence = String.Format(sentence, additionalInfo);
			}

			return sentence;
		}
	}

}