using System;
using Common;
using UnityEngine;


namespace Stamp
{
    public class Stamp : DraggableObject
    {
        [SerializeField] private bool _isStamping;
        
        protected override void Start()
        {
            base.Start();
            currentState = ObjectState.Idle;
        }

        protected override void OnMouseDrag()
        {
            base.OnMouseDrag();
            _isStamping = Input.GetAxis("Mouse Y") < -0.2f;
            //Debug.Log(Input.GetAxis("Mouse Y"));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Book.Book book = other.gameObject.GetComponent<Book.Book>();
            if (book && _isStamping)
            {
                _isStamping = false;
                
                book.OnStamped(transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Test " + other.gameObject.name);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log(other.name + " triggers stay " + name);
            if (currentState == ObjectState.Falling && other.name.Equals("Table"))
            {
                currentState = ObjectState.Idle;
                _rigidbody.Sleep();
            }
        }
    }
}