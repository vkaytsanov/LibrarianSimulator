using NPC;
using UnityEngine;

namespace UniversityCard {

	public class UniversityCardData {
		public string FirstName = "John";
		public string LastName = "Smith";
		public NpcUniversity University = NpcUniversity.NBU;
		public string Number = "F91631";
		public Sprite Photo = null;

		public UniversityCardData() { }

		public UniversityCardData(NpcData data) {
			FirstName = data.FirstName;
			LastName = data.LastName;
			University = data.University;
			Number = data.Number;
			Photo = data.Sprite;
		}
	}

}