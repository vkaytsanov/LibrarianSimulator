using Common;
using NPC;
using TMPro;
using UnityEngine;

namespace IdentificationCard {

	[System.Serializable]
	public class IdentificationCardGUI {
		public TextMeshProUGUI npcCountry;
		public TextMeshProUGUI npcName;
		public TextMeshProUGUI npcDateOfBirth;
		public TextMeshProUGUI npcSex;
		public SpriteRenderer npcPhoto;
	}

	public class IdentificationCard : Document {
		public IdentificationCardGUI IdentificationCardGUI = new IdentificationCardGUI();

		public IdentificationCardData IdentificationCardData;

		public void SpawnFromCharacter(Vector3 spawnVector, NpcData data) {
			SetupIdentity(data);
			SetupGUIIdentity();
			
			gameObject.SetActive(true);

			gameObject.transform.position = spawnVector;
			currentState = ObjectState.InitialFalling;
		}

		private void SetupIdentity(NpcData data) {
			IdentificationCardData = new IdentificationCardData(data);
		}
		
		private void SetupGUIIdentity() {
			IdentificationCardGUI.npcCountry.text = IdentificationCardData.Country;
			IdentificationCardGUI.npcName.text = IdentificationCardData.LastName + ",\n" +
			                                     IdentificationCardData.FirstName;
			IdentificationCardGUI.npcSex.text = IdentificationCardData.Sex.ToString();
			IdentificationCardGUI.npcDateOfBirth.text = IdentificationCardData.DateOfBirth.ToString();
			IdentificationCardGUI.npcPhoto.sprite = IdentificationCardData.Photo;
		}
	}

}