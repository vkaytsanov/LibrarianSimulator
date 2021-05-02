using Common;
using TMPro;
using UnityEngine;

namespace UniversityCard {
	[System.Serializable]
	public class UniversityCardGUI {
		public TextMeshProUGUI npcName;
		public TextMeshProUGUI npcUniversity;
		public TextMeshProUGUI npcNumber;
		public SpriteRenderer npcPhoto;
	}

	public class UniversityCard : Document {
		public UniversityCardGUI universityCardGUI = new UniversityCardGUI();
		private readonly UniversityCardCharacteristics _universityCardCharacteristics= new UniversityCardCharacteristics();

		public static UniversityCard Instance { get; private set; }

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
			universityCardGUI.npcName.text = _universityCardCharacteristics.LastName + ",\n" +
			                                 _universityCardCharacteristics.FirstName;

			universityCardGUI.npcUniversity.text = _universityCardCharacteristics.University;

			universityCardGUI.npcNumber.text = _universityCardCharacteristics.Number;

			universityCardGUI.npcPhoto.sprite = photo;
		}
	}

}