using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Secretary {

	public class Secretary : MonoBehaviour {
		private Animator _animator;
		private const float MovingSpeed = 2.5f;
		private bool _isLeaving = false;
		private bool _isMoving = false;
		private int _timesCalled = 0;

		// Start is called before the first frame update
		void Start() {
			_animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update() {
			if (_isMoving) {
				float dx = MovingSpeed * Time.deltaTime;
				if (_isLeaving) dx *= -2;
				transform.Translate(Vector3.left * dx);
				_animator.SetBool("moving", true);
			}
		}

		private void SetToLeaving() {
			_isMoving = true;
			_isLeaving = true;
		}

		public void SetToComing() {
			transform.position.Set(14, 4, 0);
			_isMoving = true;
			_isLeaving = false;
			_timesCalled++;
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.name == "NPC_Border") {
				_animator.SetBool("moving", false);
				_isMoving = false;
				StartCoroutine(WaitAndPickupBooks(1.0f));
			}
		}

		private void PickupBooks() {
			foreach (var book in FindObjectsOfType<Book.Book>()) {
				Destroy(book.gameObject);
			}

			SetToLeaving();
		}

		private IEnumerator WaitAndPickupBooks(float waitTime) {
			yield return new WaitForSeconds(waitTime);
			PickupBooks();
		}
	}

}