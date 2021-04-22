using System.Globalization;
using Book;
using UnityEngine;

namespace NPC {

	public class NPCManager : MonoBehaviour {
		[SerializeField] private Sprite[] sprites;

		[SerializeField] private GameObject npcPrefab;

		private NPC _npcComponent;

		private SpriteRenderer _npcSpriteRenderer;
		

		public static NPCManager Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}

		private void Start() {
			_npcComponent = npcPrefab.GetComponent<NPC>();
			_npcSpriteRenderer = npcPrefab.GetComponent<SpriteRenderer>();
			Spawn();
		}

		private void OnTriggerEnter2D(Collider2D other) {
			Spawn();
		}


		public void Spawn() {
			GenerateRandomNPCSprite();
			GenerateRandomAction();

			_npcComponent.SetToComing();
		}

		private void GenerateRandomNPCSprite() {
			_npcSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
		}

		private void GenerateRandomAction() {
			// TODO
			_npcComponent.action = NPCAction.WantingBook;

			if (_npcComponent.action == NPCAction.WantingBook)
				_npcComponent.actionInfo = BooksDB.GetRandomFictionBookCharacteristics().title;
		}

		public bool DoTitleMatch(string text) {
			return string.Compare(_npcComponent.actionInfo, text, CultureInfo.CurrentCulture,
				CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
		}
	}

}