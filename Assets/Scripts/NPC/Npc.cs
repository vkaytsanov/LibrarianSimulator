using System.Collections;
using Book;
using Dialog;
using UnityEngine;

namespace NPC {

	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public class Npc : MonoBehaviour {
		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		[SerializeField] private IdentificationCard.IdentificationCard identificationCard;
		[SerializeField] private UniversityCard.UniversityCard universityCard;
		public NpcData Data;

		private const float MovingSpeed = 5.0f;


		[HideInInspector] public int itemsToCollect;

		private bool _isMoving;


		private bool _isLeaving;


		void Start() {
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Update() {
			if (_isMoving) {
				float dx = MovingSpeed * Time.deltaTime;
				if (_isLeaving) dx *= -1;
				transform.Translate(Vector3.right * dx);
				_animator.SetBool("moving", true);
				_animator.SetBool("falling", false);
			}
		}


		private void OnTriggerEnter2D(Collider2D other) {
			if (other.name == "NPC_Border") {
				_animator.SetBool("moving", false);
				_isMoving = false;
				UseAction();
			}
		}

		private void UseAction() {
			DialogManager.Instance.StartDialog(
				new Dialogue(DialogDB.GetRandomSentence((NpcDialogueType) Data.Action.Type, Data.Action.Info.Title)));
			switch (Data.Action.Type) {
				case ActionType.WantingBook:
					universityCard.Spawn(transform.position, Data);
					itemsToCollect += 2;
					break;
				case ActionType.ReturningBook:
					universityCard.Spawn(transform.position, Data);
					BookManager.Instance.Spawn(transform.position + Vector3.right, Data.Action.Info);
					itemsToCollect += 1;
					break;
				case ActionType.Registration:
					identificationCard.SpawnFromCharacter(transform.position, Data);
					itemsToCollect += 2;
					break;
			}
		}

		public void SetToLeaving() {
			_isMoving = true;
			_isLeaving = true;
			
			// after bodyguard takes the student away
			itemsToCollect = 0;
			identificationCard.gameObject.SetActive(false);
			universityCard.gameObject.SetActive(false);
		}

		public void SetToComing(NpcData data) {
			Data = data;
			_spriteRenderer.sprite = data.Sprite;
			Debug.Log("SetToComing NPC");
			_isMoving = true;
			_isLeaving = false;
			transform.position = new Vector3(-13, 1, 0);
		}

		public IEnumerator OnAskedForIdentityCard(float stallTime) {
			yield return new WaitForSeconds(stallTime);
			int percent = Random.Range(0, 11);
			// 70% to give the card, 30% to not
			if (percent < 7) {
				identificationCard.SpawnFromCharacter(transform.position, Data);
				itemsToCollect++;
			}
			else {
				DialogManager.Instance.AddNextDialogue("I don't have it in me.", DialogSide.NpcSide);
			}
		}

		public bool IsKnockedDown() {
			_animator.SetBool("falling", true);
			return transform.position.y < -2;
		}

		public void HandleItemCollect() {
			itemsToCollect--;
			if (itemsToCollect == 0) {
				SetToLeaving();
			}
		}
		
		public void OnBookScan() {
			if (Data.Action.Info.BookReturnState == BookReturnState.Expired) {
				DialogManager.Instance.OnBookScannedFail();
			}
			else {
				DialogManager.Instance.OnBookScannedSuccess();
			}
		}
	}

}