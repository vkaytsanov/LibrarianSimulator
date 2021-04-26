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


		private readonly Queue<string> _sentences = new Queue<string>();
		private readonly Queue<DialogSide> _sides = new Queue<DialogSide>();

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

		public void StartDialog(Dialog dialog) {
			_sentences.Clear();
			_sides.Clear();

			for (int i = 0; i < dialog.Sentences.Length; i++) {
				_sentences.Enqueue(dialog.Sentences[i]);
				_sides.Enqueue(dialog.Sides[i]);
			}


			_isDialoging = true;
		}

		private void DisplayNextSentence() {
			if (_sentences.Count == 0) {
				npcDialogBox.SetActive(false);
				playerDialogBox.SetActive(false);
				npcDialogText.text = "";
				playerDialogText.text = "";
				_isDialoging = false;
				_currentTime = DialogChangeTime;
			}
			else {
				if (_sides.Dequeue() == DialogSide.NpcSide) {
					npcDialogBox.SetActive(true);
					npcDialogText.text = _sentences.Dequeue();
				}
				else {
					playerDialogBox.SetActive(true);
					playerDialogText.text = _sentences.Dequeue();
				}
			}
		}


		public void OnQuestionName(TextMeshProUGUI content) {
			_sentences.Enqueue("Is your name really " + content.text + "?");
			_sides.Enqueue(DialogSide.PlayerSide);
			_isDialoging = true;
			Debug.Log(content.text);
		}

		public void OnQuestionNumber(TextMeshProUGUI content) {
			_sentences.Enqueue("Is your number really " + content.text + "?");
			_sides.Enqueue(DialogSide.PlayerSide);
		}

		public void OnQuestionUniversity(TextMeshProUGUI content) {
			_sentences.Enqueue("Is your university really " + content.text + "?");
			_sides.Enqueue(DialogSide.PlayerSide);
		}

		public void OnQuestionPhoto() {
			_sentences.Enqueue("Is that really you on the photo?");
			_sides.Enqueue(DialogSide.PlayerSide);
		}
	}

}