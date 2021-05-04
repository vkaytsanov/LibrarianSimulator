using UnityEngine;

namespace NPC {
	public enum NpcSex : sbyte{
		Female = 0,
		Male = 1,
		Undefined = 2
	}
	public enum NpcUniversity {
		NBU,
		SU,
		TU,
		SHU,
		AU,
		UniBIT,
		RU,
		UEV
	}
	
	public class DateOfBirth {
		public int Day = 1;
		public int Month = 1;
		public int Year = 1970;
		
		public override string ToString() {
			return Day + "/" + Month + "/" + Year;
		}
	}
	
	public class NpcData {
		public string FirstName = "John";
		public string LastName = "Smith";
		public NpcUniversity University = NpcUniversity.NBU;
		public string Number = "F91631";
		public NpcSex Sex = NpcSex.Undefined;
		public DateOfBirth DateOfBirth = new DateOfBirth();
		public NpcAction Action = new NpcAction();
		public Sprite Sprite;
	}

}