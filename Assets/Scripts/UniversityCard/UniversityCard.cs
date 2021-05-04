using Common;
using NPC;
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
		private UniversityCardData _universityCardData;

		private Vector3 _spawnVector;

		protected override void Start() {
			base.Start();
			_spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(0.65f, 1.2f, 0.5f));
		}

		public void Spawn(Vector3 location, NpcData data) {
			SetupIdentity(data);
			SetupGUIIdentity();
			gameObject.SetActive(true);

			gameObject.transform.position = location;
			currentState = ObjectState.InitialFalling;
		}

		private void SetupIdentity(NpcData data) {
			_universityCardData = new UniversityCardData(data);
		}

		private void SetupGUIIdentity() {
			universityCardGUI.npcName.text = _universityCardData.LastName + ",\n" +
			                                 _universityCardData.FirstName;

			universityCardGUI.npcUniversity.text = _universityCardData.University.ToString();

			universityCardGUI.npcNumber.text = _universityCardData.Number;

			universityCardGUI.npcPhoto.sprite = _universityCardData.Photo;
		}

		public void OnRegisterNpc(Npc npc) {
			Spawn(_spawnVector, npc.Data);
		}
	}

}