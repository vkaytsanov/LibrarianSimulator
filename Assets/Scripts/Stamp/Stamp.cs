using System;
using UnityEngine;


namespace Stamp
{
    public class Stamp : DraggableObject
    {
        private bool isStamping;
        
        protected override void Start()
        {
            base.Start();
            currentState = ObjectState.Idle;
        }

        protected override void OnMouseDrag()
        {
            base.OnMouseDrag();
            isStamping = Input.GetAxis("Mouse Y") < -0.2f;
            _boxCollider.isTrigger = true;
            //Debug.Log(Input.GetAxis("Mouse Y"));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Book.Book book = other.gameObject.GetComponent<Book.Book>();
            if (book && isStamping)
            {
                _boxCollider.isTrigger = false;
                isStamping = false;
                
                book.OnStamped(transform.position);
                OnMouseExit();
            }
        }
        
        

        private void OnTriggerStay2D(Collider2D other)
        {
            if (currentState == ObjectState.Falling && other.name.Equals("Table"))
            {
                currentState = ObjectState.Idle;
                _rigidbody.Sleep();
            }
        }
    }
}