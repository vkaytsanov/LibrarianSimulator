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

		private IdentificationCardCharacteristics _identificationCardCharacteristics =
			new IdentificationCardCharacteristics();

		public static IdentificationCard Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this) {
				Destroy(gameObject);
			}
			else {
				Instance = this;
			}
		}

		public void SpawnFromCharacter(Vector3 spawnVector, Sprite photo) {
			gameObject.SetActive(true);

			gameObject.transform.position = spawnVector;
			currentState = ObjectState.InitialFalling;
			SetupIdentity(photo);
		}

		private void SetupIdentity(Sprite photo) {
			IdentificationCardGUI.npcCountry.text = _identificationCardCharacteristics.Country;
			IdentificationCardGUI.npcName.text = _identificationCardCharacteristics.LastName + ",\n" +
			                                     _identificationCardCharacteristics.FirstName;
			IdentificationCardGUI.npcSex.text = _identificationCardCharacteristics.Sex;
			IdentificationCardGUI.npcDateOfBirth.text = _identificationCardCharacteristics.DateOfBirth;
			IdentificationCardGUI.npcPhoto.sprite = photo;
		}
	}

}