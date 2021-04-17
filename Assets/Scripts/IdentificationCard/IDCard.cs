using Common;
using UnityEngine;

namespace IdentificationCard
{
    public class IDCard : DraggableObject
    {
        [SerializeField] private bool isOnRightSide;


        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            if (isOnRightSide)
            {
                ScaleUp();
            }
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            ScaleDown();
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            if (currentState == ObjectState.InitialFalling && other.name.Equals("Table"))
            {
                if (transform.position.y < other.bounds.min.y / 2.0f)
                {
                    currentState = ObjectState.Idle;
                    _rigidbody.Sleep();
                }
            }
            else if (currentState == ObjectState.Falling && other.name.Equals("Table"))
            {
                currentState = ObjectState.Idle;
                _rigidbody.Sleep();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (currentState == ObjectState.Dragged && other.name.Equals("NPC_Border"))
            {
                isOnRightSide = !isOnRightSide;
                if (isOnRightSide)
                {
                    ScaleUp();
                }
                else
                {
                    ScaleDown();
                }
            }
        }

        private void ScaleUp()
        {
            transform.localScale = Vector3.one * 0.6f;
        }

        private void ScaleDown()
        {
            transform.localScale = Vector3.one * 0.2f;
        }
    }
}