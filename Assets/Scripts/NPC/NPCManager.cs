using System.Globalization;
using Book;
using UnityEngine;

namespace NPC {

	public class NPCManager : MonoBehaviour {
		[SerializeField] private Sprite[] sprites;

		[SerializeField] private GameObject npcPrefab;

		public NPC npcComponent;

		private SpriteRenderer _npcSpriteRenderer;
		

		public static NPCManager Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}

		private void Start() {
			npcComponent = npcPrefab.GetComponent<NPC>();
			_npcSpriteRenderer = npcPrefab.GetComponent<SpriteRenderer>();
			Spawn();
		}

		private void OnTriggerEnter2D(Collider2D other) {
			Spawn();
		}


		public void Spawn() {
			GenerateRandomNPCSprite();
			GenerateRandomAction();

			npcComponent.SetToComing();
		}

		private void GenerateRandomNPCSprite() {
			_npcSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
		}

		private void GenerateRandomAction() {
			// TODO
			npcComponent.action = NPCAction.WantingBook;

			if (npcComponent.action == NPCAction.WantingBook)
				npcComponent.actionInfo = BooksDB.GetRandomFictionBookCharacteristics().title;
		}

		public bool DoTitleMatch(string text) {
			return string.Compare(npcComponent.actionInfo, text, CultureInfo.CurrentCulture,
				CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
		}
	}

}