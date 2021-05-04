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
		[HideInInspector] public NpcData Data;

		private const float MovingSpeed = 5.0f;


		public int ItemsToCollect;

		private bool _isMoving = false;


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
				new Dialogue(DialogDB.GetRandomSentence((NpcDialogueType) Data.Action.Type, Data.Action.Info)));
			switch (Data.Action.Type) {
				case ActionType.WantingBook:
					universityCard.Spawn(transform.position, Data);
					ItemsToCollect += 2;
					break;
				case ActionType.ReturningBook:
					universityCard.Spawn(transform.position, Data);
					BookManager.Instance.Spawn(transform.position + Vector3.right, Data.Action.Info);
					ItemsToCollect += 1;
					break;
				case ActionType.Registration:
					identificationCard.SpawnFromCharacter(transform.position, Data);
					ItemsToCollect += 2;
					break;
			}
		}

		public void SetToLeaving() {
			_isMoving = true;
			_isLeaving = true;
		}

		public void SetToComing(NpcData data) {
			Data = data;
			_spriteRenderer.sprite = data.Sprite;
			Debug.Log("SetToComing NPC");
			_isMoving = true;
			_isLeaving = false;
			transform.position = new Vector3(-13, 1, 0);
		}
		

		public bool IsKnockedDown() {
			_animator.SetBool("falling", true);
			return transform.position.y < -2;
		}

		public void HandleItemCollect() {
			ItemsToCollect--;
			if (ItemsToCollect == 0) {
				SetToLeaving();
			}
		}
	}

}