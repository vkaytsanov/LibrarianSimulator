using NPC;
using UnityEngine;

namespace IdentificationCard {
	
	public class IdentificationCardData {
		public string Country = "Bulgaria";
		public string FirstName = "John";
		public string LastName = "Smith";
		public NpcSex Sex = NpcSex.Undefined;
		public DateOfBirth DateOfBirth = new DateOfBirth();
		public Sprite Photo;
		
		public IdentificationCardData() { }

		public IdentificationCardData(NpcData data) {
			Country = "Bulgaria";
			FirstName = data.FirstName;
			LastName = data.LastName;
			Photo = data.Sprite;
			Sex = data.Sex;
			DateOfBirth = data.DateOfBirth;
			Photo = data.Sprite;
		}
	}

}