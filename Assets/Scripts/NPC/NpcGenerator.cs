using System;
using Book;
using Random = UnityEngine.Random;

namespace NPC {

	public static class NpcGenerator {
		private const int NumberLength = 6;

		public static NpcData GenerateRandomIdentity() {
			NpcData npcData = new NpcData();
			npcData.DateOfBirth = GenerateRandomDateOfBirth();
			npcData.Number = GenerateRandomNumber(npcData.DateOfBirth.Year);
			npcData.University = GenerateRandomUniversity();
			npcData.Sex = GenerateRandomSex();
			npcData.FirstName = GenerateRandomFirstName(npcData.Sex);
			npcData.LastName = NamesDB.GetRandomName(NameType.Family);
			return npcData;
		}

		public static string GenerateRandomNumber(int yearBorn) {
			string number = "F";

			// year 2001 - F11....
			// year 2000 - F10....

			// year 1999 - F9.....
			// year 1998 - F8.....
			// year 1997 - F7.....
			// year 1996 - F6.....

			if (yearBorn >= 2000) {
				number += 1 + yearBorn % 10;
			}
			else {
				number += yearBorn % 10;
			}

			// Fill the rest of the faculty number with random digits
			for (int i = number.Length; i < NumberLength; i++) {
				number += Random.Range(0, 9);
			}

			return number;
		}

		public static DateOfBirth GenerateRandomDateOfBirth() {
			DateOfBirth dateOfBirth = new DateOfBirth();
			dateOfBirth.Day = Random.Range(0, 31);
			dateOfBirth.Month = Random.Range(0, 13);
			dateOfBirth.Year = Random.Range(1996, 2002);
			return dateOfBirth;
		}

		/** 90% chance to be in NBU, else select random from the others */
		public static NpcUniversity GenerateRandomUniversity() {
			int percent = Random.Range(0, 100);
			if (percent < 90) {
				return NpcUniversity.NBU;
			}

			return (NpcUniversity) Random.Range(1, 8);
		}

		/** 48% to be Male, 48% to be Female, 4% to be Apache */
		public static NpcSex GenerateRandomSex() {
			int percent = Random.Range(0, 100);
			if (percent < 48) {
				return NpcSex.Male;
			}

			if (percent < 96) {
				return NpcSex.Female;
			}

			return NpcSex.Undefined;
		}

		public static string GenerateRandomFirstName(NpcSex sex) {
			if (sex == NpcSex.Undefined) {
				// roll the dice if it will be male or female name
				sex = (NpcSex) Random.Range(0, 2);
			}

			// can either be male or female, which corresponds to values 0 and 1
			// which in NameType are either male or female
			return NamesDB.GetRandomName((NameType) sex);
		}
		
		public static NpcAction GenerateRandomAction() {
			// TODO
			NpcAction action = new NpcAction();
			action.Type = ActionType.Registration;
			// npcComponent.actionType = (ActionType) Random.Range(0, 2);

			if (action.Type == ActionType.WantingBook) {
				action.Info = BooksDB.GetRandomFictionBookCharacteristics().Title;
			}
			else if (action.Type == ActionType.ReturningBook) {
				action.Info = BooksDB.GetRandomFictionBookCharacteristics().Title;
			}

			return action;
		}
	}

}