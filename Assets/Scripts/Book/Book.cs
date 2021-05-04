using System;
using UnityEngine;
using Common;
using Dialog;


namespace Book
{
    public class Book : DraggableObject
    {
        [SerializeField]
        private GameObject seal;

        private string _requestTitle;
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
                NPC.Npc npc = other.gameObject.GetComponent<NPC.Npc>();
                if (npc)
                {
                    if (currentState == ObjectState.Falling && _isStamped)
                    {
                        if (npc.Data.Action.Info.Equals(_requestTitle)) {
                            Destroy(gameObject);
                            npc.HandleItemCollect();
                        }
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

        public void OnSpawn(string bookCharacteristics) {
            _requestTitle = bookCharacteristics;
        }
    }
}