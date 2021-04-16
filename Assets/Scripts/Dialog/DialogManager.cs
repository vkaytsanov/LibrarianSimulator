using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject dialogBox;
        [SerializeField] 
        private TextMeshProUGUI dialogText;

        private bool isDialoging = false;

        private const float DIALOG_CHANGE_TIME = 4.0f;
        private float currentTime = DIALOG_CHANGE_TIME;


        private Queue<string> sentences = new Queue<string>();
        private static DialogManager _instance;

        public static DialogManager Instance
        {
            get => _instance;
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDialoging)
            {
                currentTime += Time.deltaTime;
                if (currentTime > DIALOG_CHANGE_TIME)
                {
                    currentTime = 0.0f;
                    DisplayNextSentence();
                }
            }
        }

        public void StartDialog(Dialog dialog)
        {
            sentences.Clear();

            foreach (var dialogSentence in dialog.sentences)
            {
                sentences.Enqueue(dialogSentence);
            }
            
            dialogBox.SetActive(true);
            isDialoging = true;
        }

        private void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                dialogBox.SetActive(false);
                isDialoging = false;
            }
            else
            {
                dialogText.text = sentences.Dequeue();
            }
        }
    }
}