using System;
using Book;
using Dialog;
using IdentificationCard;
using UnityEngine;

namespace NPC {

	[RequireComponent(typeof(Animator))]
	public class NPC : MonoBehaviour {
		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		private const float MovingSpeed = 5.0f;

		private bool _isMoving = true;
		public NPCAction action = NPCAction.WantingBook;
		public string actionInfo;


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
				new Dialog.Dialog(new[] {
						DialogDB.GetRandomSentence(action, actionInfo)
					}
				)
			);
			if (action == NPCAction.WantingBook) IDManager.Instance.Spawn(transform.position, _spriteRenderer.sprite);
			else if (action == NPCAction.ReturningBook) BookManager.Instance.Spawn(transform.position);
		}

		public void SetToLeaving() {
			_isMoving = true;
			_isLeaving = true;
		}

		public void SetToComing() {
			_isMoving = true;
			_isLeaving = false;
		}
	}

}