using System;
using Dialog;
using UnityEngine;

namespace NPC
{
    public class NPC : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField]
        private GameObject identificationCard;

        private bool isMoving = true;
        private const float MOVING_SPEED = 5.0f;
        private NPCIdCard _idCard = new NPCIdCard();
        private NPCAction action = NPCAction.WantingBook;
        private string actionInfo = "Three Comrades by Erich Maria Remarque";
        private Sprite sprite;

        private bool leaving = false;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (isMoving)
            {
                float dx = MOVING_SPEED * Time.deltaTime;
                if (leaving) dx *= -1;
                transform.Translate(Vector3.right * dx);
                _animator.SetBool("moving", true);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "NPC_Border")
            {
                _animator.SetBool("moving", false);
                isMoving = false;
                UseAction();
            }
        }

        private void UseAction()
        {
            DialogManager.Instance.StartDialog(
                new Dialog.Dialog(new[]
                    {
                        DialogDB.GetRandomSentence(action, actionInfo)
                    }
                )
            );

            Instantiate(identificationCard, transform.position, Quaternion.identity);

        }

        public void SetToLeaving()
        {
            isMoving = true;
            leaving = true;
        }

        public void SetToComing()
        {
            isMoving = true;
            leaving = false;
        }
    }
}