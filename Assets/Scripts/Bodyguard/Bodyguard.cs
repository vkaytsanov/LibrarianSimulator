using System.Collections;
using System.Collections.Generic;
using NPC;
using UnityEngine;

namespace Bodyguard {

	public class Bodyguard : MonoBehaviour {
		private Animator _animator;
		private AudioSource _audioSource;
		private const float MovingSpeed = 2.5f;
		private bool _isLeaving = false;
		private bool _isMoving = false;
		private bool _isAttacking = false;

		private bool _didAttacked = false;
		private float _currentAttackingTime = 0.0f;
		private const float AttackingTime = 4.0f;
		private bool _didKnockDownStudent = false;
		
		
		
		// Start is called before the first frame update
		void Start() {
			_animator = GetComponent<Animator>();
			_audioSource = GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update() {
			if (_isMoving) {
				float dx = MovingSpeed * Time.deltaTime;
				if (_isLeaving) dx *= -2;
				transform.Translate(Vector3.right * dx);
				_animator.SetBool("moving", true);
			}

			if (_isAttacking) {
				_currentAttackingTime += Time.deltaTime;
				if (!_didAttacked) {
					_audioSource.Play();
					_didAttacked = true;
				}
				
				if (_currentAttackingTime > AttackingTime / 2) {
					_didKnockDownStudent = NPCManager.Instance.npcComponent.IsKnockedDown();
					if (_didKnockDownStudent) {
						NPCManager.Instance.npcComponent.SetToLeaving();
						SetToLeaving();
					}
					
				}
			}
		}

		private void SetToLeaving() {
			_isMoving = true;
			_isLeaving = true;
			_isAttacking = false;
			_currentAttackingTime = 0.0f;
			_didAttacked = false;
		}
		
		public void SetToComing() {
			transform.position.Set(-23, 4, 0);
			_isMoving = true;
			_isLeaving = false;
			_didKnockDownStudent = false;
		}
		
		private void OnTriggerEnter2D(Collider2D other) {
			if (other.name == "NPC_Border") {
				_animator.SetBool("moving", false);
				_isMoving = false;
				_isAttacking = true;
			}
		}
	}

}