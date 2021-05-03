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

		private Vector3 _spawnVector;

		protected override void Start() {
			base.Start();
			_spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(0.65f, 1.2f, 0.5f));
		}

		public void Spawn(Vector3 location, Sprite photo) {
			gameObject.SetActive(true);
			
			gameObject.transform.position = location;
			currentState = ObjectState.InitialFalling;

			// TODO
			_universityCardCharacteristics.Photo = photo;
			SetupGUIIdentity();
		}

		private void SetupGUIIdentity() {
			universityCardGUI.npcName.text = _universityCardCharacteristics.LastName + ",\n" +
			                                 _universityCardCharacteristics.FirstName;

			universityCardGUI.npcUniversity.text = _universityCardCharacteristics.University;

			universityCardGUI.npcNumber.text = _universityCardCharacteristics.Number;

			universityCardGUI.npcPhoto.sprite = _universityCardCharacteristics.Photo;
		}

		public void OnRegisterNpc(IdentificationCard.IdentificationCard identificationCard) {
			// TODO
			_universityCardCharacteristics.FirstName = identificationCard.IdentificationCardCharacteristics.FirstName;
			_universityCardCharacteristics.LastName = identificationCard.IdentificationCardCharacteristics.LastName;
			_universityCardCharacteristics.University = "NBU";
			_universityCardCharacteristics.Number = "F91631";
			Spawn(_spawnVector, identificationCard.IdentificationCardCharacteristics.Photo);
		}
	}

}