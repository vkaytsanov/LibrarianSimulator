using System.Collections;
using NPC;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Book {

	public class BookManager : MonoBehaviour {
		[SerializeField] private Sprite[] sprites;
		[SerializeField] private GameObject bookPrefab;
		[SerializeField] private GameObject bookSeal;


		private SpriteRenderer _bookSpriteRenderer;

		private Vector3 _spawnVector;

		public static BookManager Instance { get; private set; }

		void Awake() {
			if (Instance != null && Instance != this) {
				Destroy(gameObject);
			}
			else {
				Instance = this;
			}
		}

		// Start is called before the first frame update
		void Start() {
			_bookSpriteRenderer = bookPrefab.GetComponent<SpriteRenderer>();
			_spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(0.65f, 1.2f, 0.5f));
		}

		public void OnSearchButtonClick(TMP_InputField field) {
			if (field.text == "" || !NPCManager.Instance.DoTitleMatch(field.text)) {
				Debug.Log("Book not found.");
				field.image.color = Color.red;
			}
			else {
				Spawn(_spawnVector, NPCManager.Instance.npcComponent.actionInfo);
				Debug.Log("Book found.");
				field.image.color = Color.green;
			}

			StartCoroutine(WaitAndResetFieldColor(field, 1.0f));
		}

		public static IEnumerator WaitAndResetFieldColor(TMP_InputField field, float waitTime) {
			yield return new WaitForSeconds(waitTime);
			field.image.color = Color.white;
		}

		public void Spawn(Vector3 location, string npcActionInfo) {
			_bookSpriteRenderer.sprite = GenerateRandomSprite();
			bookSeal.SetActive(false);
			GameObject spawned = Instantiate(bookPrefab, location, Quaternion.identity);
			spawned.GetComponent<Book>().OnSpawn(npcActionInfo);
		}


		Sprite GenerateRandomSprite() {
			return sprites[Random.Range(0, sprites.Length)];
		}
		
		
	}

}