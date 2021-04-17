using System;
using UnityEngine;
using Common;


namespace Book
{
    public class Book : DraggableObject
    {
        [SerializeField]
        private GameObject seal;

        private AudioSource _audioSource;
        private bool _isStamped;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            //Debug.Log(other.name + " triggers stay " + name);
            if (currentState == ObjectState.InitialFalling && other.name.Equals("Table"))
            {
                if (transform.position.y < other.bounds.min.y / 2.0f)
                {
                    currentState = ObjectState.Idle;
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.Sleep();
                    _audioSource.Play();
                }
            }
            else if ((currentState == ObjectState.Falling && other.name.Equals("Table")) || other.name.Equals("Stamp"))
            {
                currentState = ObjectState.Idle;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.Sleep();
            }
            else
            {
                NPC.NPC npc = other.gameObject.GetComponent<NPC.NPC>();
                if (npc)
                {
                    if (currentState == ObjectState.Falling && _isStamped)
                    {
                        Destroy(gameObject);
                        npc.SetToLeaving();
                    }
                }
            }
        }

        public void OnStamped(Vector2 stampPoint)
        {
            if (!_isStamped)
            {
                Debug.Log("OnStamped");
                _isStamped = true;
                seal.transform.localPosition =
                    new Vector3(stampPoint.x - transform.position.x, stampPoint.y - transform.position.y, 0) *
                    seal.transform.localScale.x;
                seal.SetActive(true);
            }
        }
    }
}