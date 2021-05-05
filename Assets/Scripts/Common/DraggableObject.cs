using UnityEngine;
using UnityEngine.Rendering;

namespace Common {

	[RequireComponent(
		typeof(SpriteRenderer),
		typeof(Rigidbody2D),
		typeof(BoxCollider2D)
	)]
	public class DraggableObject : MonoBehaviour {
		protected SpriteRenderer _spriteRenderer;
		protected Rigidbody2D _rigidbody;
		protected BoxCollider2D _boxCollider;
		private Vector3 _screenPoint;
		private Vector3 _offset;
		public ObjectState currentState = ObjectState.InitialFalling;

		protected virtual void Start() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
		}

		protected virtual void OnMouseDown() {
			_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

			_offset = gameObject.transform.position -
			         Camera.main.ScreenToWorldPoint(
				         new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));

			_rigidbody.WakeUp();

			SetOverEverything();
		}

		protected virtual void OnMouseDrag() {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
			transform.position = curPosition;
			currentState = ObjectState.Dragged;
		}


		protected virtual void OnMouseUp() {
			currentState = ObjectState.Falling;
			SetBack();
		}

		protected void SetOverEverything() {
			_spriteRenderer.sortingLayerName = "Over Everything";
			foreach (var spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
			    spriteRenderer.sortingLayerName = "Over Everything";
			}
			foreach (var spriteRenderer in gameObject.GetComponentsInChildren<Canvas>())
			{
			    spriteRenderer.sortingLayerName = "Over Everything";
			}
		}

		protected void SetBack() {
			_spriteRenderer.sortingLayerName = "Over Table";
			foreach (var spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingLayerName = "Over Table";
			}
			foreach (var spriteRenderer in gameObject.GetComponentsInChildren<Canvas>())
			{
				spriteRenderer.sortingLayerName = "Over Table";
			}
		}
	}

}