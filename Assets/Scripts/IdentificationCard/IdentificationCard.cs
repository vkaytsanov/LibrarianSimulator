using Common;
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

		public readonly IdentificationCardCharacteristics IdentificationCardCharacteristics =
			new IdentificationCardCharacteristics();
		
		public void SpawnFromCharacter(Vector3 spawnVector, Sprite photo) {
			gameObject.SetActive(true);

			gameObject.transform.position = spawnVector;
			currentState = ObjectState.InitialFalling;

			// TODO
			IdentificationCardCharacteristics.Photo = photo;
			SetupGUIIdentity();
		}

		private void SetupGUIIdentity() {
			IdentificationCardGUI.npcCountry.text = IdentificationCardCharacteristics.Country;
			IdentificationCardGUI.npcName.text = IdentificationCardCharacteristics.LastName + ",\n" +
			                                     IdentificationCardCharacteristics.FirstName;
			IdentificationCardGUI.npcSex.text = IdentificationCardCharacteristics.Sex;
			IdentificationCardGUI.npcDateOfBirth.text = IdentificationCardCharacteristics.DateOfBirth;
			IdentificationCardGUI.npcPhoto.sprite = IdentificationCardCharacteristics.Photo;
		}
	}

}