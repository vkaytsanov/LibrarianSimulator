using UnityEngine;

namespace Common {

	public class Document : DraggableObject {
		[SerializeField] private bool isOnRightSide;
		private bool _isChecked = false;

		private void OnTriggerStay2D(Collider2D other) {
			if (currentState == ObjectState.InitialFalling && other.name.Equals("Table")) {
				if (transform.position.y < other.bounds.min.y / 2.0f) {
					currentState = ObjectState.Idle;
					_rigidbody.Sleep();
				}
			}
			else if (currentState == ObjectState.Falling && other.name.Equals("Table")) {
				currentState = ObjectState.Idle;
				_rigidbody.Sleep();
			}
			else {
				NPC.NPC npc = other.gameObject.GetComponent<NPC.NPC>();
				if (npc) {
					if (currentState == ObjectState.Falling && _isChecked) {
						gameObject.SetActive(false);
						_isChecked = false;
						npc.HandleItemCollect();
					}
				}
			}
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (currentState == ObjectState.Dragged && other.name.Equals("NPC_Border")) {
				isOnRightSide = !isOnRightSide;
				if (isOnRightSide) {
					ScaleUp();
					_isChecked = true;
				}
				else {
					ScaleDown();
				}
			}
		}

		private void ScaleUp() {
			transform.localScale = Vector3.one * 0.6f;
		}

		private void ScaleDown() {
			transform.localScale = Vector3.one * 0.2f;
		}
	}

}