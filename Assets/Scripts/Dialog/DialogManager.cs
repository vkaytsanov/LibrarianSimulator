using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog {

	public class DialogManager : MonoBehaviour {
		[SerializeField] private GameObject npcDialogBox;
		[SerializeField] private GameObject playerDialogBox;
		[SerializeField] private TextMeshProUGUI npcDialogText;
		[SerializeField] private TextMeshProUGUI playerDialogText;

		private bool _isDialoging;

		private const float DialogChangeTime = 4.0f;
		private float _currentTime = DialogChangeTime;


		private readonly Queue<Dialogue> _dialogues = new Queue<Dialogue>();

		public static DialogManager Instance { get; private set; }

		// Start is called before the first frame update
		void Awake() {
			if (Instance != null && Instance != this) {
				Destroy(gameObject);
			}
			else {
				Instance = this;
			}
		}

		// Update is called once per frame
		void Update() {
			if (_isDialoging) {
				_currentTime += Time.deltaTime;
				if (_currentTime > DialogChangeTime) {
					_currentTime = 0.0f;
					DisplayNextSentence();
				}
			}
		}

		public void StartDialog(DialogueSequence dialogueSequence) {
			_dialogues.Clear();

			for (int i = 0; i < dialogueSequence.Dialogues.Length; i++) {
				_dialogues.Enqueue(dialogueSequence.Dialogues[i]);
			}
			_isDialoging = true;
		}

		public void StartDialog(Dialogue dialogue) {
			_dialogues.Clear();
			
			_dialogues.Enqueue(dialogue);

			_isDialoging = true;
		}

		private void DisplayNextSentence() {
			if (_dialogues.Count == 0) {
				npcDialogBox.SetActive(false);
				playerDialogBox.SetActive(false);
				npcDialogText.text = "";
				playerDialogText.text = "";
				_isDialoging = false;
				_currentTime = DialogChangeTime;
			}
			else {
				Dialogue dialogue = _dialogues.Dequeue();
				if (dialogue.DialogSide == DialogSide.NpcSide) {
					npcDialogBox.SetActive(true);
					npcDialogText.text = dialogue.Sentence;
				}
				else {
					playerDialogBox.SetActive(true);
					playerDialogText.text = dialogue.Sentence;
				}
			}
		}

		private void AddNextDialogue(string text, DialogSide side = DialogSide.PlayerSide) {
			_dialogues.Enqueue(new Dialogue(text, side));
			_isDialoging = true;
		}

		public void OnQuestionName(TextMeshProUGUI content) {
			AddNextDialogue("Is your name really " + content.text + "?");
		}

		public void OnQuestionNumber(TextMeshProUGUI content) {
			AddNextDialogue("Is your number really " + content.text + "?");
		}

		public void OnQuestionUniversity(TextMeshProUGUI content) {
			AddNextDialogue("Is your university really " + content.text + "?");
		}

		public void OnQuestionPhoto() {
			AddNextDialogue("Is that really you on the photo?");
		}

		public void OnQuestionSex(TextMeshProUGUI content) {
			string fullSex = content.text == "M" ? "male" : "female";
			AddNextDialogue("Are you a " + fullSex + "?");
		}

		public void OnQuestionCountry(TextMeshProUGUI content) {
			AddNextDialogue("Are you really from " + content.text + "?");
		}

		public void OnQuestionDateOfBirth(TextMeshProUGUI content) {
			AddNextDialogue("When are you born?");
		}

		public void OnBookScannedSuccess() {
			AddNextDialogue("Everything is okay, you are good to go.");
		}

		public void OnBookScannedFail() {
			AddNextDialogue("You are late.");
			AddNextDialogue("Late with what?", DialogSide.NpcSide);
			AddNextDialogue("Returning the book, you need to pay 3 Euro");
			AddNextDialogue("Erhh... I don't have money in me.", DialogSide.NpcSide);
		}
	}

}