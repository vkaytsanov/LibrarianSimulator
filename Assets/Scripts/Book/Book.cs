using System;
using UnityEngine;

namespace Book
{
    public class Book : DraggableObject
    {
        [SerializeField]
        private GameObject seal;
        private bool isStamped = false;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (currentState == ObjectState.InitialFalling && other.name.Equals("Table"))
            {
                if (transform.position.y < other.bounds.min.y / 2.0f)
                {
                    currentState = ObjectState.Idle;
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.Sleep();
                }
            }
            else if (currentState == ObjectState.Falling && other.name.Equals("Table"))
            {
                currentState = ObjectState.Idle;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.Sleep();
            }
            else if (other.name.Equals("Stamp"))
            {
                
            }
            else
            {
                NPC.NPC npc = other.gameObject.GetComponent<NPC.NPC>();
                if (npc)
                {
                    if (currentState == ObjectState.Falling && isStamped)
                    {
                        Destroy(gameObject);
                        npc.SetToLeaving();
                        Debug.Log("Bye");
                    }
                }
            }
        }

        public void OnStamped(Vector2 stampPoint)
        {
            isStamped = true;
            seal.transform.localPosition =
                new Vector3(stampPoint.x - transform.position.x, stampPoint.y - transform.position.y, 0);
            seal.SetActive(true);
            
        }
    }
}