using System.Globalization;
using Book;
using UnityEngine;

namespace NPC {

	public class NpcManager : MonoBehaviour {
		[SerializeField] private Sprite[] sprites;
		[HideInInspector] public Npc npc;

		public static NpcManager Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}

		private void Start() {
			Spawn();
		}

		private void OnTriggerEnter2D(Collider2D other) {
			Spawn();
		}


		public void Spawn() {
			NpcData data = NpcGenerator.GenerateRandomIdentity();
			data.Sprite = sprites[Random.Range(0, sprites.Length)];
			npc.SetToComing(data);
		}


		public bool DoTitleMatch(string text) {
			return string.Compare(npc.Data.Action.Info.Title, text, CultureInfo.CurrentCulture,
				CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
		}
	}

}