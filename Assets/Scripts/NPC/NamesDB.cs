using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Random = UnityEngine.Random;

namespace NPC {

	public enum NameType : sbyte{
		Male,
		Female,
		Family
	}
	
	public class NamesDB {
		private const int TypesCount = 3;
		private static readonly List<string>[] Names = new List<string>[TypesCount];
	
		static NamesDB() {
			for (int i = 0; i < TypesCount; i++) {
				Names[i] = new List<string>();
			}
			ParseSentences(Names[(int) NameType.Male], "males");
			ParseSentences(Names[(int) NameType.Female], "females");
			ParseSentences(Names[(int) NameType.Family], "family");
		}

		private static void ParseSentences(List<string> db, string fileName) {
			StreamReader sr = File.OpenText("Assets/Database/Names/" + fileName + ".txt");
			string sentence;
			while ((sentence = sr.ReadLine()) != null) {
				db.Add(sentence);
			}

			sr.Close();
		}

		public static string GetRandomName(NameType type) {
			int idx = (int) type;
			return Names[idx][Random.Range(0, Names[idx].Count)];
		}
	}

}